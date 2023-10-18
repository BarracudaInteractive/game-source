using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth; //Max health
    [SerializeField] private float damage; //Default damage
    [SerializeField] private float minDamage; //Minimun damage to detect damages

    private Vector3 lookAt; //Vector that saves the forward direction, but normlized

    private float health; //actual health
    private float speed; //actual speed
    
    private bool grounded; //Determinates if car in ground

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; //Set actual health o maximun health
        speed = 0; 
        lookAt = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //speed = this.GetComponent<MovementScript>().speed;
        lookAt = transform.forward.normalized;

        if (!Physics.Raycast(transform.position, Vector3.down, 0.001f)) //If teh car separates a bit from the ground
            grounded = false; //The car won't be grounded
    }

    //Method invoked when car collides with any other thing
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Road" && !grounded) //If it falls and land on the road
        {
            float degrees = GetDegreesDifference(lookAt, Vector3.down); //Calculates entrance angle

            float s = Math.Abs(speed * lookAt.y);
            float d = (s / 100) * (damage / (degrees / 22.5f) + 1f); //Calculates total damage
            if (d > minDamage) //Only if damage is grater than minimun damage
                health -= d; //It applays damage
        }

        if (collision.gameObject.tag == "Road" || collision.gameObject.tag == "Out of Road") //If car collides with ground
            grounded = true;
        
        if (collision.gameObject.tag == "Wall") //If it collides with a wall, same thing as the fall damage
        {
            float degrees = GetDegreesDifference(collision.collider);

            float d = (speed / 100) * (damage / (degrees / 22.5f) + 1f);
            if (d > minDamage)
                health -= d;
        }
    }

    //Method that calculates entrance angle with collider
    float GetDegreesDifference(Collider collider)
    {
        Vector3 oPosition = collider.transform.position;

        Vector3 v = oPosition - transform.position;

        return Vector3.Angle(v, lookAt /* * Math.Sign(car.GetComponent<MovementScript>().speed)*/);
    }

    //Same thing than before, but general
    float GetDegreesDifference(Vector3 v1, Vector3 v2)
    {
        return Vector3.Angle(v1, v2);
    }

    //Method to get total damage for GUI
    public float GetDamage()
    {
        return maxHealth - health;
    }
}
