using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The Witch enemy type teleports randomly within a given boundary and shoots projectiles toward the player.
/// </summary>
public class Witch : EnemyHitTarget
{
    [Header("Movement Wizard")]
    [SerializeField] private float moveInterval = 2.0f;
    private float moveTimer;

    [Header("Attack")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float shotRate = 2.1f;
    private float shotTimer;

    [Header("Teleportation Boundary")]
    [SerializeField] private Vector3 minimum = new Vector3(-10f, 1f, -10f);
    [SerializeField] private Vector3 maximum = new Vector3(10f, 10f, 10f);

    protected override void Update()
    {
        base.Update();
        Attack();
    }

    protected override void Move()
    {
        RandomMove();
    }

    private void RandomMove()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer > moveInterval)
        {
            float randomX = UnityEngine.Random.Range(minimum.x, maximum.x);
            float randomY = UnityEngine.Random.Range(minimum.y, maximum.y);
            float randomZ = UnityEngine.Random.Range(minimum.z, maximum.z);
            transform.position = new Vector3(randomX, randomY, randomZ);

            moveTimer = 0;
        }
    }

    protected override void Attack()
    {
        if (projectilePrefab == null) return;

        shotTimer += Time.deltaTime;
        if (shotTimer > shotRate)
        {
            CreateProjectile();
            shotTimer = 0f;
        }
    }

    private void CreateProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity); //TODO: Use Object poolig
        projectile.damage = attackDamage;
        projectile.target = playerTarget;

    }
}
