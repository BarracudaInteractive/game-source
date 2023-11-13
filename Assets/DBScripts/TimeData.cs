using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeData
{
    private float _allSeconds; // Stores all amount of seconds
    private float _seconds; // Stores remaining seconds
    private int _minutes; // Stores whole minutes of time
    // Default builder
    public TimeData() 
    {
        _allSeconds = 0;
        _DivideSeconds();
    }
    // Another builder
    public TimeData(float allSeconds) 
    {
        this._allSeconds = allSeconds; // Total seconds are passed
        _DivideSeconds(); // Time it's divided into minutes and seconds
    }
    // Divides total time into minutes and seconds
    private void _DivideSeconds()
    {
        _minutes = (int)_allSeconds / 60;
        _seconds = _allSeconds - _minutes * 60;
    }
    // Returns time in minutes and seconds for and easy UI display
    public (int minutes, float seconds) ReturnTime()
    {
        return (_minutes, _seconds);
    }
    // Updates time with a new amount of seconds
    public void UpdateSeconds(float seconds)
    {
        this._allSeconds = seconds;
        _DivideSeconds();
    }
    // Returns total time for database update
    public float GetAllSeconds() { return _allSeconds; }
    // Returns total time as an string
    override public string ToString()
    {
        return _allSeconds.ToString();
    }
}
