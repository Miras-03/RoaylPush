using PlayerSpace;
using UnityEngine;

namespace EnemySpace.Attack
{
    public abstract class AttackAbility
    {
        protected Rigidbody rb;
        protected Animator anim;
        protected Player player;
        protected readonly int damageValue;
        protected readonly int throwForce;

        public AttackAbility(Rigidbody rb, Animator anim, Player player, int damageValue = 5, int throwForce = 1200)
        {
            this.rb = rb;
            this.anim = anim;
            this.player = player;
            this.damageValue = damageValue;
            this.throwForce = throwForce;
        }

        public abstract void CheckOrExecuteAttack(float distance);
    }
}