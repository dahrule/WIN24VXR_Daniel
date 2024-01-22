using System.Collections;
using UnityEngine;

/// <summary>
/// Code retrived from: Unity VR - Bow And Arrow by Fist Full of Shrimp. https://www.youtube.com/watch?v=wKmtG_nKsQ0
/// </summary>
public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Transform tip;

    private Rigidbody rigidBody;
    private bool inAir=false;
    private Vector3 lastPosition = Vector3.zero;
    private float destroyDelay = 4f;


    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        PullInteraction.PullActionReleased += Release;

        Stop();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        PullInteraction.PullActionReleased-= Release;
        gameObject.transform.parent = null;
        inAir = true;
        SetPhysics(true);

        Vector3 force= transform.forward*value*speed;
        rigidBody.AddForce(force,ForceMode.Impulse);

        StartCoroutine(RotateWithVelocity());

        lastPosition = tip.position;
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while(inAir)
        {
            Quaternion newrotation = Quaternion.LookRotation(rigidBody.velocity, transform.up);
            transform.rotation = newrotation;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if(inAir)
        {
            CheckCollision();
            lastPosition = tip.position;
        }
    }

    private void CheckCollision()
    {
        if (Physics.Linecast(lastPosition, tip.position, out RaycastHit hitInfo))
        {

            if (hitInfo.transform.gameObject.layer != 8)
            {
                if (hitInfo.transform.TryGetComponent(out Rigidbody rbody))
                {
                    if (rbody.TryGetComponent<HitTarget>(out HitTarget hitTarget))
                    {
                        hitTarget.TakeHit(1);
                        return;
                    }

                    rigidBody.interpolation = RigidbodyInterpolation.None;
                    transform.parent = hitInfo.transform;
                    rbody.AddForce(rigidBody.velocity, ForceMode.Impulse);
                }
                Stop();
                StartCoroutine(DestroyAfterDelay(destroyDelay));
            }
        }
    }



    private void Stop()
    {
        inAir = false;
        SetPhysics(false);
    }

    private void SetPhysics(bool usePhysics)
    {
        rigidBody.useGravity = usePhysics;
        rigidBody.isKinematic = !usePhysics;
    }

    private void OnBecameInvisible()
    {
        //Destroy(transform.GetChild(0).gameObject); // This will destroy the first child GameObject
        Debug.Log("arrow out of sight");

    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
