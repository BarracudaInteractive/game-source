using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovComponentFH : MonoBehaviour
{
    [SerializeField] private float height; //Set camera height
    [SerializeField] private float movSpeed; //Set camera movement speed
    [SerializeField] private float rotSpeed; //Set camera rotation speed

    [SerializeField] private Vector3 rotInit; //Set camera initial rotation

    //Method executed when created
    void Start()
    {
        movSpeed *= 0.001f; //Transformation for easy view numbers
        rotSpeed *= 0.01f; //Same as above

        transform.rotation = Quaternion.identity; //Reset camera rotation
        transform.Rotate(rotInit); //Set camera rotation to specified
    }

    //Method executed evey frame
    void Update()
    {
        this.GoToHeight(); //Firstly, check height
        this.Move(); //Secondly, move the camera
        this.Rotate(); //Finally, rotate the camera
    }

    //Check if camera height is ok
    private void GoToHeight()
    {
        //If camera is height above collider, it drops
        if (Physics.Raycast(transform.position, Vector3.down, height))
        {
            transform.Translate(new Vector3(0, 0.001f, 0));
        } //If camera is height below collider, it goes up
        else if (!Physics.Raycast(transform.position, Vector3.down, height+0.002f))
        {
            transform.Translate(new Vector3(0, -0.001f, 0));
        }
    }

    //Method to move the camera
    private void Move() 
    { 
        //A to move left
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-transform.right * movSpeed);
        }

        //D to move right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(transform.right * movSpeed);
        }

        //W to move forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(transform.forward * movSpeed);
        }

        //S to move backwards
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-transform.forward * movSpeed);
        }
    }

    //Method to rotate camera
    private void Rotate()
    {
        //Use Q to rotate counte-clock wise
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(new Vector3(0, -rotSpeed, 0));
        }

        //Use E to rotate clock wise
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(new Vector3(0, rotSpeed, 0));
        }
    }
}
