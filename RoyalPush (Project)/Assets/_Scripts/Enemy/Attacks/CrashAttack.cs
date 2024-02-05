using UnityEngine;
using PlayerSpace;
using System.Threading.Tasks;

namespace EnemySpace.Attack
{
    public sealed class CrashAttack : AttackAbility
    {
        public CrashAttack(Rigidbody rb, Animator anim, Player player, int damageValue = 5, int throwForce = 1200 ) : 
            base(rb, anim, player, damageValue, throwForce) { }

        public override async void CheckOrExecuteAttack(float distance)
        {
            if (!anim.GetBool(nameof(CrashAttack)))
            {
                anim.SetTrigger(nameof(CrashAttack));
                await Task.Delay(500);
                player.TakeDamage(damageValue);
                player.Throw(throwForce, direction: Vector3.back, timeToGetConscious: 2);
            }
        }
    }
}