using HealthSpace;
using PlayerSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace EnemySpace.Attack
{
    public sealed class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        private Animator anim;
        private Player player;
        private AttackAbility currentAttack;
        private PunchAttack punchAttack;
        private CrashAttack crashAttack;
        private UppercutAttack uppercutAttack;
        private Dictionary<int, AttackAbility> attackabilities = new Dictionary<int, AttackAbility>();

        [SerializeField] private LayerMask playerMask;
        [SerializeField] private int respiringTime = 5;
        [SerializeField] private int powerGatherTime = 5;
        private const int discoverDistance = 15;
        private bool isRespiring = false;

        [Inject]
        public void Construct(Player player) => this.player = player;

        private void Awake()
        {
            anim = GetComponent<Animator>();

            punchAttack = new PunchAttack(rb, anim, player);
            uppercutAttack = new UppercutAttack(rb, anim, player, damageValue: 20, throwForce: 900);
            crashAttack = new CrashAttack(rb, anim, player,transform, damageValue: 40, throwForce: 1000);
        }

        private IEnumerator Start()
        {
            SetRandAttackAbility();
            while (true)
            {
                while (!isRespiring)
                {
                    yield return new WaitForSeconds(powerGatherTime);
                    isRespiring = true;
                }
                yield return new WaitForSeconds(respiringTime);
                SetRandAttackAbility();
                isRespiring = false;
            }
        }

        private void OnEnable()
        {
            attackabilities.Add(0, punchAttack);
            attackabilities.Add(1, uppercutAttack);
            attackabilities.Add(2, crashAttack);

            player.OnDeath += () => Destroy(this);
        }

        private void OnDestroy() => attackabilities.Clear();

        private void FixedUpdate() => HitOrAttack();

        private void HitOrAttack()
        {
            Vector3 fwd = transform.TransformDirection(Vector3.forward);
            RaycastHit hit;
            Vector3 target = new Vector3(transform.position.x, 3, transform.position.z);
            if (Physics.Raycast(target, fwd, out hit, discoverDistance, playerMask))
                CheckOrAttack(hit.distance);
        }

        public void CheckOrAttack(float distance) => currentAttack.CheckOrExecuteAttack(distance);

        private void SetRandAttackAbility()
        {
            int index = Random.Range(0, attackabilities.Count);
            currentAttack = attackabilities[2];
        }
    }
}