using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(AudioSource))]
public class LaserAngularVelocityReader : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;
    XRGrabInteractable grabInteractable;
    ControllerData controllerData = null;
    AudioSource audioSource = null;
    [SerializeField] float swingTreshold = 2f;



    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1;//Make sound fully 3d
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(GetControllerData);
    }


    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(GetControllerData);
    }

    //This a callback function 
    private void GetControllerData(SelectEnterEventArgs arg0)
    {
        //Gets the parent of the interactor that interacts with this object.
        Transform parent=arg0.interactorObject.transform.parent;

       controllerData = parent.GetComponent<ControllerData>();
        
        
    }

    void PlaySwingSound()
    {
        if (audioSource == null) return;
        audioSource.PlayOneShot(audioClip);
    }

    void Update()
    {
        //Play sound only when laser's angularvelocity is above a treshold and there is no sound playing.
        if(IsLaserMoving() && !audioSource.isPlaying)
        {
            PlaySwingSound();
        }
            
    }

    private bool IsLaserMoving()
    {
        if(controllerData==null) return false;

        return controllerData.AngularVelocity.magnitude > swingTreshold;

    }
}
