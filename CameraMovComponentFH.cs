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

    [SerializeField] private GameObject camera;

    //Method executed when created
    void Start()
    {
        movSpeed *= 0.001f; //Transformation for easy view numbers
        rotSpeed *= 0.01f; //Same as above

        transform.position = new Vector3 (transform.position.x, height, transform.position.z);

        transform.rotation = Quaternion.identity; //Reset camera rotation
        camera.transform.rotation = Quaternion.identity;
        camera.transform.Rotate(new Vector3(rotInit.x, 0, rotInit.z)); //Set camera rotation to specified
        transform.Rotate(new Vector3(0, rotInit.y, 0));
    }

    //Method executed evey frame
    void Update()
    {
        this.Move(); //Secondly, move the camera
        this.Rotate(); //Finally, rotate the camera
        Debug.Log(transform.forward);
    }

    //Method to move the camera
    private void Move() 
    { 
        //A to move left
        if (Input.GetKey(KeyCode.A))
        {
            transform.position+= -transform.right * movSpeed;
        }

        //D to move right
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * movSpeed;
        }

        //W to move forward
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * movSpeed;
        }

        //S to move backwards
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += -transform.forward * movSpeed;
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
