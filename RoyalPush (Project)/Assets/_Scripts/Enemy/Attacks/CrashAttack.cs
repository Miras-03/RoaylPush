using UnityEngine;
using PlayerSpace;
using System.Threading.Tasks;

namespace EnemySpace.Attack
{
    public sealed class CrashAttack : AttackAbility
    {
        Transform transform;

        public CrashAttack(Rigidbody rb, Animator anim, Player player, Transform transform, int damageValue = 5, int throwForce = 1200) :
            base(rb, anim, player, damageValue, throwForce)
        { 
            this.transform = transform;
        }


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