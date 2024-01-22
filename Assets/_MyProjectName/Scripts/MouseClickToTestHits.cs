using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickToTestHits : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        TestHitTarget();
    }

    private void TestHitTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent<HitTarget>(out HitTarget hitTarget))
                {
                    hitTarget.TakeHit(1);
                }
            }
        }
    }
}

