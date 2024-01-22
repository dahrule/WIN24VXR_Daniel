using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Code retrived from: Unity VR - Bow And Arrow by Fist Full of Shrimp. https://www.youtube.com/watch?v=wKmtG_nKsQ0
/// </summary>
public class PullInteraction : XRBaseInteractable
{
    public static event Action<float> PullActionReleased;
    public Transform start,end;
    public GameObject notch;
    public float PullAmout { get; private set; } = 0.0f;

    private LineRenderer lineRenderer;
    private IXRSelectInteractor pullingInteractor=null;

    protected override void Awake()
    {
        base.Awake();
        lineRenderer = GetComponent<LineRenderer>();
    }

    //Kooked up in the editor from Unity events on the XRInteractable object/Interactable Events/Select/SelectEntered
    public void SetPullingInteractor(SelectEnterEventArgs args)
    {
        pullingInteractor = args.interactorObject;
    }

    //Hooked up in the editor from Unity events on the XRInteractable object/Interactable Events/Select/SelectExited
    public void Release()
    {
        PullActionReleased?.Invoke(PullAmout);
        pullingInteractor = null;
        PullAmout = 0f;
        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x,notch.transform.localPosition.y,0f);
        UpdateBowString();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);
        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic) 
        {
            if(isSelected)
            {
                Vector3 pullPosition = pullingInteractor.transform.position;
                PullAmout = CalculatePull(pullPosition);

                UpdateBowString();
            }
        }
    }

    private float CalculatePull(Vector3 pullPosition)
    {
        Vector3 pullDirection = pullPosition - start.position;
        Vector3 targetDirection=end.position-start.position;
        float maxLength = targetDirection.magnitude;

        targetDirection.Normalize();
        float pullValue = Vector3.Dot(pullDirection, targetDirection) / maxLength;

        return Mathf.Clamp(pullValue, 0,1);
    }

    private void UpdateBowString()
    {
        Vector3 linePosition = Vector3.forward * Mathf.Lerp(start.transform.localPosition.z,end.transform.localPosition.z,PullAmout);
        notch.transform.localPosition = new Vector3(notch.transform.localPosition.x,notch.transform.localPosition.y,linePosition.z+0.2f);
        lineRenderer.SetPosition(1,linePosition);
    }
}
