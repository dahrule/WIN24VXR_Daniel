using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyHitTarget : HitTarget
{
    [SerializeField] int hitsToDestroy = 1;
    [SerializeField] Transform playerTarget;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float attackRate = 3f;

    private int currentHits = 0;

    protected void Awake()
    {
        if (playerTarget == null)
        {
            playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

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

    private void Attack()
    {
        Debug.Log(this.gameObject.name + " is Attacking Player",this.gameObject);
    }

    public override void TakeHit(int hitpower)
    {
        currentHits += hitpower;
        if (currentHits>= hitsToDestroy)
        {
            base.TakeHit(hitpower);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            InvokeRepeating(nameof(Attack), 1f, attackRate);
        }

    }
}
