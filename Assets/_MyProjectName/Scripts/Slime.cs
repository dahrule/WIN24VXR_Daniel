using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
        Vector3 position = transform.position;
        Instantiate(enemyPrefab, position+Vector3.right,Quaternion.identity);
        Instantiate(enemyPrefab, position + Vector3.left, Quaternion.identity);
    }
}
