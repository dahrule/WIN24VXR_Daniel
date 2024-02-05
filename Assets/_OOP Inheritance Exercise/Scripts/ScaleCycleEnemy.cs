using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class ScaleCycleEnemy : EnemyHitTarget
{
    public float scaleSpeed = 1.0f; 
    public float minScale=0.5f;
    public float maxScale=2f;

    protected override void Update()
    {
        CycleScale();
    }
    void CycleScale()
    {
        float pingPongValue = Mathf.PingPong(Time.time * scaleSpeed, 1f); //Create a smoot oscillation between 0, 1
        float scale = Mathf.Lerp(minScale, maxScale, pingPongValue);//Interpolate between minScale and maxScale based on the pingPongValue.

        transform.localScale = new Vector3(scale, scale, scale);
    }
}

