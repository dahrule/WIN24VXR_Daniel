using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// An EnemyHitTarget is a HitTarget that has Move and Attack behaviours.
/// </summary>
public class EnemyHitTarget : HitTarget
{
    [SerializeField] float attackRate = 3f;
    [SerializeField] protected int attackDamage = 1;
    [SerializeField] int hitsToDestroy = 1;
    [SerializeField] public Transform playerTarget;

    Coroutine attackCoroutine = null;
   
    private int currentHits = 0;
    [SerializeField] protected float moveSpeed = 1f;

    protected virtual void Update()
    {
        Move();
    }
    protected override int CalculateScore()
    {
        int scoreGain = hitsToDestroy * baseScore;
        return scoreGain;
    }

    protected virtual void Move()
    {
        if (playerTarget == null) return;

        // Move our position a step closer to the target.
        var step = moveSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, step);
    }

    protected virtual void Attack()
    {
        Debug.Log(gameObject.name+" is Attacking Player every " + attackRate + " seconds",gameObject);
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
            //InvokeRepeating(nameof(Attack), 1f, attackRate);
            attackCoroutine=StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine() 
    {
        WaitForSeconds waitTime = new(attackRate);
        while(true)
        {
            Attack();
            yield return waitTime;
        }  
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("exited ");
            if(attackCoroutine!=null) StopCoroutine(attackCoroutine);
        }
           
    }
}
