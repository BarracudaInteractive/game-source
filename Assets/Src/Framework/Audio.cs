using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class Audio : MonoBehaviour
{
    // This script reads some of the car's current properties and plays sounds accordingly.
    // The engine sound can be a simple single clip which is looped and pitched, or it
    // can be a crossfaded blend of four clips which represent the timbre of the engine
    // at different RPM and Throttle state.

    // the engine clips should all be a steady pitch, not rising or falling.

    // when using four channel engine crossfading, the four clips should be:
    // lowAccelClip : The engine at low revs, with throttle open (i.e. begining acceleration at very low speed)
    // highAccelClip : Thenengine at high revs, with throttle open (i.e. accelerating, but almost at max speed)
    // lowDecelClip : The engine at low revs, with throttle at minimum (i.e. idling or engine-braking at very low speed)
    // highDecelClip : Thenengine at high revs, with throttle at minimum (i.e. engine-braking at very high speed)

    // For proper crossfading, the clips pitches should all match, with an octave offset between low and high.

    public enum EngineAudioOptions
    {
        Simple,
        FourChannel
    }

    [Header("Engine sound style")] public EngineAudioOptions engineSoundStyle = EngineAudioOptions.FourChannel;
    public AudioClip lowAccelClip;
    public AudioClip lowDecelClip;
    public AudioClip highAccelClip;
    public AudioClip highDecelClip;

    private readonly float _fPitchMultiplier = 1f;
    private readonly float _fLowPitchMin = 1f;
    private readonly float _fLowPitchMax = 6f;
    private readonly float _fHighPitchMultiplier = 0.25f;
    private readonly float _fMaxRolloffDistance = 500;
    private readonly float _fDopplerLevel = 1;
    private readonly bool _useDoppler = true;

    private float _fCamDist = .0f;
    private AudioSource _mLowAccel;
    private AudioSource _mLowDecel;
    private AudioSource _mHighAccel;
    private AudioSource _mHighDecel;
    private bool _hasStartedSound;

    private Controller _Controller;
    private InputManager _InputManager;

    private void _StartSound()
    {
        // setup the simple audio source
        _mHighAccel = _SetUpEngineAudioSource(highAccelClip);

        // if we have four channel audio setup the four audio sources
        if (engineSoundStyle == EngineAudioOptions.FourChannel)
        {
            _mLowAccel = _SetUpEngineAudioSource(lowAccelClip);
            _mLowDecel = _SetUpEngineAudioSource(lowDecelClip);
            _mHighDecel = _SetUpEngineAudioSource(highDecelClip);
        }

        // flag that we have started the sounds playing
        _hasStartedSound = true;
    }

    private void _StopSound()
    {
        //Destroy all audio sources on this object:
        foreach (var source in GetComponents<AudioSource>()) Destroy(source);
        _hasStartedSound = false;
    }

    // sets up and adds new audio source to the gane object
    private AudioSource _SetUpEngineAudioSource(AudioClip clip)
    {
        // create the new audio source component on the game object and set up its properties
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0;
        source.spatialBlend = 1;
        source.loop = true;

        // start the clip from a random point
        source.time = Random.Range(0f, clip.length);
        source.Play();
        source.minDistance = 5;
        source.maxDistance = _fMaxRolloffDistance;
        source.dopplerLevel = 0;
        return source;
    }

    // unclamped versions of Lerp and Inverse Lerp, to allow value to exceed the from-to range
    private float _ULerp(float from, float to, float value)
    {
        return (1.0f - value) * from + value * to;
    }

    private void FixedUpdate()
    {
        _Controller = GetComponent<Controller>();
        _InputManager = GetComponent<InputManager>();
        // get the distance to main camera
        float camDist = (Camera.main.transform.position - transform.position).sqrMagnitude;

        // stop sound if the object is beyond the maximum roll off distance
        if (_hasStartedSound && camDist > _fMaxRolloffDistance * _fMaxRolloffDistance)
            _StopSound();

        // start the sound if not playing and it is nearer than the maximum distance
        if (!_hasStartedSound && camDist < _fMaxRolloffDistance * _fMaxRolloffDistance)
            _StartSound();

        if (_hasStartedSound)
        {
            // The pitch is interpolated between the min and max values, according to the car's revs.
            float pitch = _ULerp(_fLowPitchMin, _fLowPitchMax, _Controller.GetEngineRPM / _Controller.GetMaxRPM);

            // clamp to minimum pitch (note, not clamped to max for high revs while burning out)
            pitch = Mathf.Min(_fLowPitchMax, pitch);

            if (engineSoundStyle == EngineAudioOptions.Simple)
            {
                // for 1 channel engine sound, it's oh so simple:
                _mHighAccel.pitch = pitch * _fPitchMultiplier * _fHighPitchMultiplier;
                _mHighAccel.dopplerLevel = _useDoppler ? _fDopplerLevel : 0;
                _mHighAccel.volume = 1;
            }
            else
            {
                // for 4 channel engine sound, it's a little more complex:
                // adjust the pitches based on the multipliers
                _mLowAccel.pitch = pitch * _fPitchMultiplier;
                _mLowDecel.pitch = pitch * _fPitchMultiplier;
                _mHighAccel.pitch = pitch * _fHighPitchMultiplier * _fPitchMultiplier;
                _mHighDecel.pitch = pitch * _fHighPitchMultiplier * _fPitchMultiplier;
                float accFade = 0;

                // get values for fading the sounds based on the acceleration
                accFade = Mathf.Abs((_InputManager.GetVertical > 0 && !_Controller.IsAcc) ? _InputManager.GetVertical : 0);

                float decFade = 1 - accFade;

                // get the high fade value based on the cars revs
                float highFade = Mathf.InverseLerp(0.2f, 0.8f, _Controller.GetEngineRPM / 10000);
                float lowFade = 1 - highFade;

                // adjust the values to be more realistic
                highFade = 1 - ((1 - highFade) * (1 - highFade));
                lowFade = 1 - ((1 - lowFade) * (1 - lowFade));
                accFade = 1 - ((1 - accFade) * (1 - accFade));
                decFade = 1 - ((1 - decFade) * (1 - decFade));

                // adjust the source volumes based on the fade values
                _mLowAccel.volume = lowFade * accFade;
                _mLowDecel.volume = lowFade * decFade;
                _mHighAccel.volume = highFade * accFade;
                _mHighDecel.volume = highFade * decFade;

                // adjust the doppler levels
                _mHighAccel.dopplerLevel = _useDoppler ? _fDopplerLevel : 0;
                _mLowAccel.dopplerLevel = _useDoppler ? _fDopplerLevel : 0;
                _mHighDecel.dopplerLevel = _useDoppler ? _fDopplerLevel : 0;
                _mLowDecel.dopplerLevel = _useDoppler ? _fDopplerLevel : 0;
            }
        }
    }
}