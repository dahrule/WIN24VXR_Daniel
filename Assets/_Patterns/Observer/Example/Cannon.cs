using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// This is mostly a simple 'Detect Input' and 'Fire Projectile'. This is mostly for showcasing the mechanics
/// and triggering damage events by creating projectiles on key press
/// </summary>
namespace Examples.Observer
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField] Projectile _projectile = null;
        [SerializeField] Transform _projectileSpawnPoint = null;

        private IObjectPool<Projectile> projectilePool;

        private void Awake()
        {
            projectilePool = new ObjectPool<Projectile>(CreateProjectile, OnGet, OnRelease, OnDestroyProjectile, maxSize: 5);
        }

        private void OnDestroyProjectile(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }

        private void OnRelease(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
        }

        private void OnGet(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
            projectile.transform.position = _projectileSpawnPoint.position;
        }

        private Projectile CreateProjectile()
        {
            Projectile projectile = Instantiate(_projectile, _projectileSpawnPoint.position, transform.rotation);
            projectile.SetPool(projectilePool);
            return projectile; ;
        }

        private void Update()
        {
            // fire  missiles
            if (Input.GetKeyDown(KeyCode.Space))
            {
                projectilePool.Get(); 
                //FireProjectile();
            }
        }

        void FireProjectile()
        {
            Projectile newProjectile = Instantiate(_projectile, _projectileSpawnPoint.position, transform.rotation);
        }
    }
}

