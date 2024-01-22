using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Slime creates two child enemies when destroyed.
/// </summary>
public class Slime : EnemyHitTarget
{
    [SerializeField] EnemyHitTarget enemyPrefab;
    protected override void Destroy()
    {
        base.Destroy();
        SpawnChildren();
    }

    private void SpawnChildren()
    {
        EnemyHitTarget newEnemy;
        Vector3 position = transform.position;

        // Create the first child enemy to the right of the parent.
        newEnemy = Instantiate(enemyPrefab, position+Vector3.right,Quaternion.identity);
        newEnemy.playerTarget = playerTarget;

        // Create the first child enemy to the left of the parent
        newEnemy = Instantiate(enemyPrefab, position + Vector3.left, Quaternion.identity);
        newEnemy.playerTarget = playerTarget;

    }
}
