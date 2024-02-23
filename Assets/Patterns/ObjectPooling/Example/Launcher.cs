//#define OBJECT_POOL

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// This is mostly a simple 'Detect Input' and 'Fire Projectile'.
/// </summary>
namespace Examples.ObjectPooling
{
    public class Launcher : MonoBehaviour
    {
        [SerializeField] Projectile _projectile = null;
        [SerializeField] Transform _projectileSpawnPoint = null;

        #if OBJECT_POOL
        private IObjectPool<Projectile> projectilePool;

        private void Awake()
        {
            projectilePool = new ObjectPool<Projectile>(CreateProjectile, OnGet, OnRelease, OnDestroyProjectile, maxSize: 5);
        }

        private Projectile CreateProjectile()
        {
            Projectile projectile = Instantiate(_projectile, _projectileSpawnPoint.position, transform.rotation);
            projectile.SetPool(projectilePool);
            return projectile;
        }

        private void OnGet(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
            projectile.transform.position = _projectileSpawnPoint.position;
        }

        private void OnRelease(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void OnDestroyProjectile(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }
        #endif

        private void Update()
        {
            // fire missiles
            if (Input.GetKeyDown(KeyCode.Space))
            {
                #if OBJECT_POOL
                projectilePool.Get();
                #else
                FireProjectile();
                #endif
            }
        }

        void FireProjectile()
        {
            Instantiate(_projectile, _projectileSpawnPoint.position, transform.rotation); // TODO consider Object Pooling here
        }
    }
}


