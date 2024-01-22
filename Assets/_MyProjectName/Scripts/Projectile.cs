using UnityEngine;

public class Projectile: MonoBehaviour
{
    public int damage = 1;
    public Transform target;
    private float shotSpeed = 3.0f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, shotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }
}