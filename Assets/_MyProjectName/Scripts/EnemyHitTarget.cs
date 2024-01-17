using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitTarget : HitTarget
{
    [SerializeField] int hitsToDestroy = 1;
    [SerializeField] Transform playerTarget;


    protected override void Update()
    {
        base.Update();
        Move();
    }
    protected override int CalculateScore()
    {
        int scoreGain = hitsToDestroy * baseScore;
        return scoreGain;
    }

    private void Move()
    {
        
    }
}
