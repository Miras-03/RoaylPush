using System.Collections.Generic;
using UnityEngine;

public sealed class Player : MonoBehaviour
{
    [SerializeField] private Transform enemyTransform;
    private Rigidbody rb;
    private Animator anim;
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnim;
    private List<Rigidbody> bones;

    private Vector3 movementDirection = Vector3.zero;

    private void Awake()
    {
        bones = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
        rb = GetComponent<Rigidbody>();
        anim =GetComponent<Animator>();
        playerMovement = new PlayerMovement(rb, transform, enemyTransform);
        playerAnim = new PlayerAnimation(anim);
    }

    private void Start()
    {
        PushDown(false);
        ResetRigidbodyProp();
    }

    private void FixedUpdate()
    {
        playerMovement.MovePlayer(out movementDirection);
        AnimateMove();
    }

    private void ResetRigidbodyProp()
    {
        rb.isKinematic = false;
        rb.useGravity = false;
    }

    private void PushDown(bool shouldFall)
    {
        foreach (var b in bones)
            b.isKinematic = !shouldFall;
    }

    private void AnimateMove()
    {
        playerAnim.Run(Mathf.Abs(movementDirection.z) > 0.1f ? Mathf.Sign(movementDirection.z) : 0f);
        playerAnim.Turn(Input.GetAxis("Horizontal"));
    }
}