using UnityEngine;

namespace Client.Scripts.Zombie.States
{
    public abstract class BaseZombieState
    {
        protected readonly Animator Animation;
        protected readonly IZombieSwitchState ZombieSwitchState;

        protected BaseZombieState(Animator animation, IZombieSwitchState zombieSwitchState)
        {
            Animation = animation;
            ZombieSwitchState = zombieSwitchState;
        }

        public abstract void Start();
        public abstract void Stop();
        public abstract void Action();
    }
}
