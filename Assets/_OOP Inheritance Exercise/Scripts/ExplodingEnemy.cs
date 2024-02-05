using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : EnemyHitTarget
{
    [SerializeField] float explosionRadius = 5f;
    [SerializeField] GameObject explosionRadiusVisualizer;

    protected override void Start()
    {
        base.Start();
        VisualizeExplosionRadius(false);
    }

    void Explode()
    {
        //Create a sphere raycast
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);

        //Print the name of all objects that collided with the raycast and look for enemy targets to apply damage.
        foreach (Collider hitCollider in hitColliders)
        {
            Debug.Log("Collided with: " + hitCollider.name);
            if(hitCollider.gameObject.TryGetComponent(out IHittable hittable))
            {
                hittable.TakeHit(attackDamage);
            }
        }

        Destroy(gameObject);
    }

    private void VisualizeExplosionRadius(bool isVisible)
    {
        Vector3 scale = new Vector3(explosionRadius * 2f, explosionRadius * 2f, explosionRadius * 2f);
        explosionRadiusVisualizer.transform.localScale = scale;

        explosionRadiusVisualizer.SetActive(isVisible);
    }

    protected override void Destroy()
    {
        VisualizeExplosionRadius(true);
        Invoke(nameof(Explode), 3f); // call the explode method after 3 seconds. You can replace this with a coroutine.
       
    }
}
