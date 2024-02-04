using EnemySpace;
using HealthSpace;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace PlayerSpace
{
    public sealed class Player : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI hpIndicator;
        private Transform enemyTransform;
        private Rigidbody rb;
        private Animator anim;
        private PlayerMovement playerMovement;
        private PlayerAnimation playerAnim;
        private Health health;
        private List<Rigidbody> bones;

        [SerializeField] private int maxHP = 100;
        private Vector3 movementDirection = Vector3.zero;

        [Inject]
        public void Construct(Enemy enemy) => enemyTransform = enemy.transform;

        private void Awake()
        {
            bones = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            playerMovement = new PlayerMovement(rb, transform, enemyTransform);
            playerAnim = new PlayerAnimation(anim);
            health = new Health(maxHP);
            
        }

        private void Start()
        {
            PushDown(false);
            ResetRigidbodyProp();
        }

        private void OnEnable()
        {
            health.Add(new PlayerHealthObserver(hpIndicator));
            SetHealth(5);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                anim.SetTrigger("Punch");
        }

        private void FixedUpdate()
        {
            playerMovement.MovePlayer(out movementDirection);
            AnimateMove();
        }

        public void SetHealth(int takeValue) => health.TakeHealth -= takeValue;

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
}