using PlayerSpace;
using System.Threading.Tasks;
using UnityEngine;

namespace EnemySpace.Attack
{
    public sealed class UppercutAttack : AttackAbility
    {
        private const int discoverDistance = 4;

        public UppercutAttack(Rigidbody rb, Animator anim, Player player, int damageValue = 5, int throwForce = 1200) : 
            base(rb, anim, player, damageValue, throwForce ) { }

        public override async void CheckOrExecuteAttack(float distance)
        {
            if (!anim.GetBool(nameof(UppercutAttack))&&distance < discoverDistance)
            {
                anim.SetTrigger(nameof(UppercutAttack));
                await Task.Delay(300);
                player.TakeDamage(damageValue);
                player.Throw(throwForce, direction: Vector3.up, timeToGetConscious: 3);
            }
        }
    }
}