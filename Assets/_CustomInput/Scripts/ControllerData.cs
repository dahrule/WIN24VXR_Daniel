using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerData : MonoBehaviour
{
    [SerializeField] InputActionProperty angularVelocityProperty;
    [SerializeField] InputActionProperty velocityProperty;

     //public Vector3 angularVelocity;
     public Vector3 velocity;


    public Vector3 AngularVelocity { private set; get; }
    

    // Update is called once per frame
    void Update()
    {
        AngularVelocity = angularVelocityProperty.action.ReadValue<Vector3>();
        //Debug.Log("angular veocity: "+ angularVelocity);
        velocity = velocityProperty.action.ReadValue<Vector3>();
        //Debug.Log("veocity: " + velocity);

    }
}
