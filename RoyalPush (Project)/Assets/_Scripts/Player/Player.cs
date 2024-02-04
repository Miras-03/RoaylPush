using EnemySpace;
using HealthSpace;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlayerSpace
{
    public sealed class Player : MonoBehaviour, IDieable
    {
        [SerializeField] private Slider hpBar;
        private Transform enemyTransform;
        private Rigidbody rb;
        private Animator anim;
        private PlayerMovement playerMovement;
        private PlayerAnimation playerAnim;
        private Health health;
        private List<Rigidbody> bones;

        [SerializeField] private int maxHP = 10;
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
            PushDown(true);
            ResetRigidbodyProp();
        }

        private void OnEnable()
        {
            health.AddHPObserver(new HealthObserver(health, hpBar));
            health.AddDeathObserver(this);
        }

        private void OnDestroy()
        {
            health.ClearHPObservers();
            health.RemoveDeathObserver(this);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                anim.SetTrigger("Punch");
        }

        private void FixedUpdate()
        {
            /*playerMovement.MovePlayer(out movementDirection);
            AnimateMove();*/
        }

        public void TakeDamage(int takeValue) => health.TakeHealth -= takeValue;

        private void ResetRigidbodyProp()
        {
            rb.isKinematic = false;
            rb.useGravity = false;
        }

        private void PushDown(bool shouldFall)
        {
            anim.enabled = !shouldFall;
            foreach (var b in bones)
                b.isKinematic = !shouldFall;
        }

        private void AnimateMove()
        {
            playerAnim.Run(Mathf.Abs(movementDirection.z) > 0.1f ? Mathf.Sign(movementDirection.z) : 0f);
            playerAnim.Turn(Input.GetAxis("Horizontal"));
        }

        public void ExecuteDeath()
        {
            Destroy(hpBar.gameObject);
            PushDown(true);
        }

        public Health Health => health;
    }
}