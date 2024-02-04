using UnityEngine;
using Zenject;
using EnemySpace;

namespace PlayerSpace
{
    public sealed class PlayerMovement
    {
        private Rigidbody rb;
        private Transform transform;
        private Transform enemyTransform;

        private const int distanceToDiscover = 3;

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

            if (HasDistanceToTarget())
            {
                if (Input.GetKey(KeyCode.W))
                    movementDirection += transform.forward;
                else if (Input.GetKey(KeyCode.S))
                    movementDirection -= transform.forward;

                if (Input.GetKey(KeyCode.A))
                    movementDirection -= transform.right;
                else if (Input.GetKey(KeyCode.D))
                    movementDirection += transform.right;

                rb.velocity = movementDirection * 5f;
            }
            else
                rb.velocity = -targetDirection * 5f;

            return movementDirection;
        }

        private bool HasDistanceToTarget() => Vector3.Distance(transform.position, enemyTransform.position) > distanceToDiscover;
    }
}