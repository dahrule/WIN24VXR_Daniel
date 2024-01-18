using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// An EnemyHitTraget is a HITtRAGET that has move and attack behaviours.
/// </summary>
public class EnemyHitTarget : HitTarget
{
    [SerializeField] float attackRate = 3f;
    [SerializeField] int hitsToDestroy = 1;
    [SerializeField] Transform playerTarget;
   
    private int currentHits = 0;
    [SerializeField] float moveSpeed=1f;

    protected void Awake()
    {
        
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

    protected void Move()
    {
        if (playerTarget == null) return;

        // Move our position a step closer to the target.
        var step = moveSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, step);

        //TODO: fix jittering when reaching target, test if the distance is smaller than a stop distance.
    }

    private void Attack()
    {

        Debug.Log(gameObject.name+" is Attacking Player",gameObject);
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
        if(other.gameObject.CompareTag("Player"))
        {
            InvokeRepeating(nameof(Attack), 1f, attackRate);
        }
        

    }
}
