using UnityEngine;

namespace PlayerSpace
{
    public sealed class PlayerAnimation
    {
        private Animator anim;

        public PlayerAnimation(Animator anim) => this.anim = anim;

        public void Run(float speed) => anim.SetFloat("Run", speed);

        public void Turn(float axis) => anim.SetInteger("TurnInt", (int)axis);
    }
}