using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyHitTarget : HitTarget
{
    [SerializeField] int hitsToDestroy = 1;
    [SerializeField] Transform playerTarget;
    [SerializeField] float moveSpeed = 1;

    private int currentHits = 0;


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
        if (playerTarget == null) return;

        // Move our position a step closer to the target.
        var step = moveSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, step);

        //TODO: fix jittering when reaching target, test if the distance is smaller than a stop distance.
    }

    public override void TakeHit(int hitpower)
    {
        currentHits += hitpower;
        if (currentHits>= hitsToDestroy)
        {
            base.TakeHit(hitpower);
        }
    }
}
