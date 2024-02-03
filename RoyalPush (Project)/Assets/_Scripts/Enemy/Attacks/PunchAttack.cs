using UnityEngine;

namespace EnemySpace.Attack
{
    public sealed class PunchAttack : IAttackAbility
    {
        private Animator anim;

        public PunchAttack(Animator anim) => this.anim = anim;

        public void ExecuteAttack()
        {
            anim.SetTrigger(nameof(PunchAttack));
        }
    }
}