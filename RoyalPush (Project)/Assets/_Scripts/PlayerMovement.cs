using UnityEngine;

public sealed class PlayerMovement 
{
    private Rigidbody rb;
    private Transform transform;
    private Transform enemyTransform;

    public PlayerMovement(Rigidbody rb, Transform transform, Transform enemyTransform)
    {
        this.rb = rb;
        this.transform = transform;
        this.enemyTransform = enemyTransform;
    }

    public void MovePlayer(out Vector3 movementDirection)
    {
        movementDirection = Vector3.zero;
        movementDirection = CalculateAndMove(movementDirection);
    }

    private Vector3 CalculateAndMove(Vector3 movementDirection)
    {
        Vector3 targetDirection = enemyTransform.position - transform.position.normalized;
        targetDirection.y = 0f;

        if (DistanceToTarget() > 3f)
        {
            if (Input.GetKey(KeyCode.W))
                movementDirection += targetDirection;
            else if (Input.GetKey(KeyCode.S))
                movementDirection -= targetDirection;

            if (Input.GetKey(KeyCode.A))
                movementDirection += Vector3.Cross(targetDirection, Vector3.up);
            else if (Input.GetKey(KeyCode.D))
                movementDirection -= Vector3.Cross(targetDirection, Vector3.up);

            rb.velocity = movementDirection * 5f;
        }
        else
            rb.velocity = -targetDirection * 5f;

        return movementDirection;
    }

    private float DistanceToTarget() => Vector3.Distance(transform.position, enemyTransform.position);
}