using UnityEngine;

namespace EnemySpace.Attack
{
    public sealed class CrashAttack : IAttackAbility
    {
        private Animator anim;

        public CrashAttack(Animator anim) => this.anim = anim;

        public void ExecuteAttack()
        {
            anim.SetTrigger(nameof(CrashAttack));
        }
    }
}