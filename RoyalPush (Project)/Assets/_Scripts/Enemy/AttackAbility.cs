using PlayerSpace;
using UnityEngine;

namespace EnemySpace.Attack
{
    public abstract class AttackAbility
    {
        protected Rigidbody rb;
        protected Animator anim;
        protected Player player;
        protected int damageValue;

        public AttackAbility(Rigidbody rb, Animator anim, Player player, int damageValue = 5)
        {
            this.rb = rb;
            this.anim = anim;
            this.player = player;
            this.damageValue = damageValue;
        }

        public abstract void CheckOrExecuteAttack(float distance);
    }
}