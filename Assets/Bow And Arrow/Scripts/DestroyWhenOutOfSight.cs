using UnityEngine;

public class DestroyWhenOutOfSight : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(transform.parent.gameObject);

    }
}
