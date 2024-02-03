using UnityEngine;

namespace EnemySpace.Attack
{
    public sealed class RangedAttack : IAttackAbility
    {
        private Animator anim;

        public RangedAttack(Animator anim) => this.anim = anim;

        public void ExecuteAttack()
        {
            anim.SetTrigger(nameof(RangedAttack));
        }
    }
}