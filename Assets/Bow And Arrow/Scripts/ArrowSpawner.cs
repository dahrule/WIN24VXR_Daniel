using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Code retrived from: Unity VR - Bow And Arrow by Fist Full of Shrimp. https://www.youtube.com/watch?v=wKmtG_nKsQ0
/// </summary>
public class ArrowSpawner : MonoBehaviour
{
    public GameObject arrow;
    public GameObject notch;

    private XRGrabInteractable bow;
    private bool arrowNotched = false;
    private GameObject currentArrow = null;


    // Start is called before the first frame update
    void Start()
    {
        bow=GetComponent<XRGrabInteractable>();
        PullInteraction.PullActionReleased += (float value) => { NotchEmpty();};
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= (float value) => { NotchEmpty(); };
    }

    // Update is called once per frame
    void Update()
    {
        if(bow.isSelected && arrowNotched==false)
        {
            arrowNotched = true;
            StartCoroutine(DelaySpawn());
        }
        if(!bow.isSelected && currentArrow!=null)
        {
            Destroy(currentArrow);
            NotchEmpty();
        }
    }

    private void NotchEmpty()
    {
        arrowNotched=false;
        currentArrow=null;
    }

    IEnumerator DelaySpawn()
    {
        yield return new WaitForSeconds(1f);
        currentArrow=Instantiate(arrow, notch.transform);
    }
}
