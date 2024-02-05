using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IHittable
{
    [SerializeField] Rigidbody[] wallRigidBodies = null;
    public void TakeHit(int damage)
    {
        CollapseWall();
    }

    private void CollapseWall()
    {
        foreach (var wallRgB in wallRigidBodies)
        {
            if (wallRgB != null)
                wallRgB.isKinematic = false;
        }

        Invoke(nameof(Destroy), 3f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}

