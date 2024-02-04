using UnityEngine;
using PlayerSpace;

namespace EnemySpace.Attack
{
    public sealed class CrashAttack : AttackAbility
    {
        public CrashAttack(Rigidbody rb, Animator anim, Player player, int damageValue = 5) : base(rb, anim, player, damageValue) { }

        public override void CheckOrExecuteAttack(float distance)
        {
            if (!anim.GetBool(nameof(CrashAttack)))
            {
                player.TakeDamage(damageValue);
                anim.SetTrigger(nameof(CrashAttack));
                rb.AddForce(Vector3.back * 5, ForceMode.Impulse);
            }
        }
    }
}