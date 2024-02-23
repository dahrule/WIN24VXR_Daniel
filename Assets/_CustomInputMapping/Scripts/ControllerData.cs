using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ControllerData : MonoBehaviour
{
    [SerializeField] InputActionProperty angularVelocityProperty;

    public Vector3 AngularVelocity { get; private set; }



    // Update is called once per frame
    void Update()
    {
        AngularVelocity=angularVelocityProperty.action.ReadValue<Vector3>();
        Debug.Log(AngularVelocity);
    }
}
