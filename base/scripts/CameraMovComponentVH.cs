using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovComponentVH : MonoBehaviour
{
    [SerializeField] private float minHeight; //Set camera min height
    [SerializeField] private float maxHeight; //Set camera max height
    [SerializeField] private float movSpeed; //Set camera movement speed
    [SerializeField] private float rotSpeed; //Set camera rotation speed
    [SerializeField] private float heightSpeed; //Set camera changing height speed

    [SerializeField] private Vector3 rotInit; //Set camera initial rotation
    void Start()
    {
        movSpeed *= 0.001f; //Transformation for easy view numbers
        rotSpeed *= 0.01f; //Same as above

        transform.Translate(new Vector3(0, (minHeight + maxHeight) / 2, 0));

        transform.rotation = Quaternion.identity; //Reset camera rotation
        transform.Rotate(rotInit); //Set camera rotation to specified
    }

    // Update is called once per frame
    void Update()
    {
        this.ChangeHeight(); //Firstly, change the camera height
        this.Move(); //Secondly, move the camera
        this.Rotate(); //Finally, rotate the camera
    }

    //Change the camera height with mouse
    private void ChangeHeight()
    {
        //If camera height isn't equal to minHeight and use the mouse scroll down, camera will go down
        if (transform.position.y > minHeight && Input.mouseScrollDelta.y == -1)
        {
            transform.Translate(new Vector3(0, -heightSpeed, 0));
        }

        //If camera height isn't equal to maxHeight and use the mouse scroll up, camera will go up
        if (transform.position.y < maxHeight && Input.mouseScrollDelta.y == 1)
        {
            transform.Translate(new Vector3(0, heightSpeed, 0));
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
