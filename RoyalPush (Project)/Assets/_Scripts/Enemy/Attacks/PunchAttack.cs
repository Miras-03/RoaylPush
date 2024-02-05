using PlayerSpace;
using UnityEngine;

namespace EnemySpace.Attack
{
    public sealed class PunchAttack : AttackAbility
    {
        private const int discoverDistance = 3;

        public PunchAttack(Rigidbody rb, Animator anim, Player player, int damageValue = 5) : 
            base(rb, anim, player, damageValue) { }

        public override void CheckOrExecuteAttack(float distance)
        {
            if (!anim.GetBool(nameof(PunchAttack))&&distance<discoverDistance)
            {
                anim.SetTrigger(nameof(PunchAttack));
                player.TakeDamage(damageValue);
            }
        }
    }
}