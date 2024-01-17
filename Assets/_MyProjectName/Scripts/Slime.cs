using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime :EnemyHitTarget
{
    [SerializeField] EnemyHitTarget EnemyPrefab;

    protected override void Destroy()
    {
        base.Destroy();
        SpawnChildEnemies();
    }

    private void SpawnChildEnemies()
    {
        if (EnemyPrefab == null) return;

        var position = transform.position;
        Instantiate(EnemyPrefab, position + Vector3.right, Quaternion.identity);
        Instantiate(EnemyPrefab, position + Vector3.left, Quaternion.identity);
    }
}
