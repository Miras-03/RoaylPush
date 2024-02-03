using UnityEngine;
using EnemySpace.Attack;
using Zenject;
using Player;

namespace EnemySpace
{
    public sealed class Enemy : MonoBehaviour
    {
        private IAttackAbility currentAttack;
        private Transform playerTransform;
        private Rigidbody rb;

        private const int distanceToDiscover = 5;

        [Inject]
        public void Constructor(Player.Player player) => playerTransform = player.transform;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate() => DetectOrAttack();

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