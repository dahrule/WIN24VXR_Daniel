using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CircularMovementEnemy : EnemyHitTarget
{
    [SerializeField] float radius = 5f; // Radius of the circular path
    private float angle = 0f;
    //public Vector3 offset = new(1, 0, 1);

   Vector3 positionATsTART= Vector3.zero;

    protected override void Start()
    {
        base.Start();
        positionATsTART=this.transform.position;
    }
    protected override void Move()
    {
        MoveAroundPlayerUsingRotateAround();
    }

    void MoveAroundPlayer()
    {
        // Calculate the new position in a circular path
        float x = playerTarget.position.x + Mathf.Cos(angle) * radius;
        float z = playerTarget.position.z + Mathf.Sin(angle) * radius;

        // Set the object's position to the calculated values
        transform.position = new Vector3(x, transform.position.y, z);

        // Increment the angle based on time and speed
        angle += moveSpeed * Time.deltaTime;

    }

    void MoveAroundPlayerUsingRotateAround()
    {
        // Calculate the desired position in a circular path
        float angle = Time.time * moveSpeed; // Use Time.time to make it time-dependent
        Vector3 offset = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;
        Vector3 desiredPosition = playerTarget.position + offset;

        // Set the object's position to the desired position
        transform.position = desiredPosition;

        // Rotate around the playerTarget
        transform.RotateAround(playerTarget.position, Vector3.up, moveSpeed * Time.deltaTime);

    }
}
