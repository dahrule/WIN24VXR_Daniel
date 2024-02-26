//#define OBJECT_POOL

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// This class is a simple projectile 
/// </summary>
namespace Examples.ObjectPooling
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float _travelSpeed = 5f;

        private Rigidbody _rigidbody = null;

        #if OBJECT_POOL
        private IObjectPool<Projectile> projectilePool;
        public void SetPool(IObjectPool<Projectile>pool)
        {
             projectilePool = pool;
        }
        #endif

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Travel(_rigidbody);
        }

        protected void Travel(Rigidbody rb)
        {
            Vector3 moveOffset = transform.forward * _travelSpeed;
            rb.MovePosition(rb.position + moveOffset);
        }

        private void OnBecameInvisible()
        {
            #if OBJECT_POOL
            projectilePool.Release(this);
            #else
            Destroy(gameObject); //TODO consider Object Pooling here
            #endif
        }
    }
}

