using EnemySpace;
using HealthSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlayerSpace
{
    public sealed class Player : MonoBehaviour, IDieable
    {
        public Action OnDeath;
        [SerializeField] private Slider hpBar;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private ParticleSystem punch;
        private Transform enemyTransform;
        private Transform hipsBone;
        private Rigidbody rb;
        private Animator anim;
        private PlayerMovement playerMovement;
        private PlayerAnimation playerAnim;
        private Enemy enemy;
        private Health health;
        private List<Rigidbody> bones;

        [SerializeField] private LayerMask enemyMask;
        [SerializeField] private int maxHP = 100;
        private Vector3 movementDirection = Vector3.zero;
        private const int damageValue = 10;
        private const int discoverDistance = 3;
        private bool hasDied = false;

        [Inject]
        public void Construct(Enemy enemy)
        {
            this.enemy = enemy;
            enemyTransform = enemy.transform;
        }

        private void Awake()
        {
            bones = new List<Rigidbody>(GetComponentsInChildren<Rigidbody>());
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            playerMovement = new PlayerMovement(rb, transform, enemyTransform);
            playerAnim = new PlayerAnimation(anim);
            health = new Health(maxHP);
            hipsBone = anim.GetBoneTransform(HumanBodyBones.Hips);
        }

        private void Start()
        {
            EnableAnimAndKinematic(true);
            EnableKinematicOfMainRigidbody(false);
        }

        private void OnEnable()
        {
            health.AddHPObserver(new HealthObserver(health, hpBar, hpText));
            health.AddDeathObserver(this);
        }

        private void OnDestroy()
        {
            health.ClearHPObservers();
            health.RemoveDeathObserver(this);
        }

        private void Update()
        {
            bool canPunch = Input.GetMouseButtonDown(0) && !anim.GetBool("Punch");
            if (canPunch)
            {
                anim.SetTrigger("Punch");
                HitAndAttack();
            }
        }

        private void FixedUpdate()
        {
            bool hasConscious = anim.enabled;
            if (hasConscious)
            {
                playerMovement.MovePlayer(out movementDirection);
                AnimateMove();
            }
        }

        private void HitAndAttack()
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            Vector3 target = new Vector3(transform.position.x, 3, transform.position.z);
            if (Physics.Raycast(target, fwd, discoverDistance, enemyMask))
                enemy.TakeDamage(damageValue);
        }

        private void AnimateMove()
        {
            playerAnim.Run(Mathf.Abs(movementDirection.z) > 0.1f ? Mathf.Sign(movementDirection.z) : 0f);
            playerAnim.Turn(Input.GetAxis("Horizontal"));
        }

        public void TakeDamage(int damage)
        {
            health.TakeHealth -= damage;
            punch.Play();
        }

        public void ExecuteDeath()
        {
            hasDied = true;
            Destroy(hpBar.gameObject);
            Destroy(hpText.gameObject);
            Throw(800, Vector3.back);
        }

        public void Throw(int force, Vector3 direction, float timeToGetConscious = -1)
        {
            EnableAnimAndKinematic(false);
            AddForceToBones(force, direction);
            EnableKinematicOfMainRigidbody(true);
            if (timeToGetConscious > 0 && !hasDied) 
                StartCoroutine(GetConscious(timeToGetConscious));
        }

        private IEnumerator GetConscious(float time)
        {
            yield return new WaitForSeconds(time);
            AlignPositionToHips();
            EnableAnimAndKinematic(true);
            EnableKinematicOfMainRigidbody(false);
        }

        private void EnableAnimAndKinematic(bool enabled)
        {
            anim.enabled = enabled;
            foreach (var b in bones)
                b.isKinematic = enabled;
        }

        private void EnableKinematicOfMainRigidbody(bool enabled) => rb.isKinematic = enabled;

        private void AddForceToBones(int force, Vector3 direction)
        {
            foreach (var b in bones)
                b.AddForce(direction * force);
        }

        private void AlignPositionToHips()
        {
            Vector3 originalHipsPos = hipsBone.position;
            transform.position = hipsBone.position;
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
                transform.position = new Vector3(transform.position.x, hitInfo.point.y, transform.position.z);
            hipsBone.position = originalHipsPos;
        }

        public Health Health => health;
    }
}