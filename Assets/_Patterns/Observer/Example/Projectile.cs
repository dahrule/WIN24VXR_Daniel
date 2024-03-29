﻿using Examples.ObjectPooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// This class is a simple projectile that is the catalyst for triggering a Damaged event.
/// This is also a fairly typical scenario of the type of way you may implement damage in your games.
/// </summary>
namespace Examples.Observer
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float _travelSpeed = 5f;
        [SerializeField] int _damage = 20;

        Rigidbody _rigidbody = null;
        private IObjectPool<Projectile> projectilePool;

        public void SetPool(IObjectPool<Projectile> pool)
        {
            projectilePool = pool;
        }

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

        private void OnCollisionEnter(Collision other)
        {
            Health health = other.gameObject.GetComponent<Health>();
            health.TakeDamage(_damage);

            projectilePool.Release(this);
            //Destroy(gameObject); //TODO consider Object Pooling here
        }
    }
}

