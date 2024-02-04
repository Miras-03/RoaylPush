using PlayerSpace;
using UnityEngine;

namespace EnemySpace.Attack
{
    public sealed class UppercutAttack : AttackAbility
    {
        public UppercutAttack(Rigidbody rb, Animator anim, Player player, int damageValue = 5) : base(rb, anim, player, damageValue) { }

        public override void CheckOrExecuteAttack(float distance)
        {
            if (!anim.GetBool(nameof(UppercutAttack)))
            {
                player.TakeDamage(damageValue);
                anim.SetTrigger(nameof(UppercutAttack));
                rb.AddForce(Vector3.back*50, ForceMode.Impulse);
            }
        }
    }
}