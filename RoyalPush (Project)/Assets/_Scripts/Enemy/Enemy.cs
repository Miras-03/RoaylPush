using EnemySpace.Attack;
using HealthSpace;
using TMPro;
using UnityEngine;
using Zenject;

namespace EnemySpace
{
    public sealed class Enemy : MonoBehaviour
    {
        private IAttackAbility currentAttack;
        [SerializeField] private TextMeshProUGUI hpIndicator;
        private Health health;
        private Transform playerTransform;
        private Rigidbody rb;

        [SerializeField] private int maxHP = 100;
        private const int distanceToDiscover = 5;

        [Inject]
        public void Constructor(PlayerSpace.Player player) => playerTransform = player.transform;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            health = new Health(maxHP);
        }

        private void OnEnable()
        {
            health.Add(new EnemyHealthObserver(hpIndicator));
            health.TakeHealth -= 10;
        }

        private void FixedUpdate() => DetectOrAttack();

        public void SetHealth(int takeValue) => health.TakeHealth = takeValue;

        public void SetAttackAbility(IAttackAbility attack) => currentAttack = attack;

        private void DetectOrAttack()
        {
            if (IsPlayerDiscovered())
                Attack();
        }

        private bool IsPlayerDiscovered() => Vector3.Distance(transform.position, playerTransform.position) < distanceToDiscover;

        private void Attack() => currentAttack.ExecuteAttack();
    }
}