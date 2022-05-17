using UnityEngine;

namespace Client.Scripts.Zombie.States
{
    public class ZombieIdleState : BaseZombieState
    {
        public ZombieIdleState(Animator animation, IZombieSwitchState zombieSwitchState) :
            base(animation, zombieSwitchState)
        {
        }

        public override void Start()
        {
        }

        public override void Stop()
        {
        }

        public override void Action()
        {
           
        }
    }
}
