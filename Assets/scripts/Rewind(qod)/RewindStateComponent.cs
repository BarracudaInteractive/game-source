using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindStateComponent : MonoBehaviour
{
    [SerializeField] private GameObject car; //Reference to real time car, via link in editor
    State actualState; //Atribute that saves the actual state
    State prevState; //Atribute that saves the previous state

    void Start()
    {
        actualState = new State();
        prevState = new State();
    }

    //Method that saves the state when the car arrives at checkpoint. It has to be called in checkpint 0 (the exit)
    public void SaveState()
    {
        //Saving state changes if it's checkpint 0 or anyone else
        if (prevState.EqualsZero() && actualState.EqualsZero())
        {
            //actualState = new State(car position, car direction, car speed);
        }
        else
        {
            prevState = actualState;
            //actualState = new State(car position, car direction, car speed);
        }
    }

    //Method to change to a previous checkpoint, unless car is in checkpoint 0
    public void RewindState()
    {
        if (!prevState.EqualsZero())
        {
            actualState = prevState;
            //Use actualState atributes to change car atributes
        }
    }
}
