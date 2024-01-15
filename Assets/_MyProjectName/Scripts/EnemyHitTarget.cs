using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitTarget : HitTarget
{
    [SerializeField] int hitsToDestroy = 1;
   
    protected override int CalculateScore()
    {
        int scoreGain = hitsToDestroy * score;
        Debug.Log("GainedScore for EnemyTarget: "+ scoreGain);
        return scoreGain;
    }
   

  
}
