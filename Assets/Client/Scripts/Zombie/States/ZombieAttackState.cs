using System.Threading.Tasks;
using UnityEngine;

namespace Client.Scripts.Zombie.States
{
    public class ZombieAttackState : BaseZombieState
    {
        private readonly ZombieAttackDetector _attackDetector;
        private float _damage;


        public ZombieAttackState(Animator animation, IZombieSwitchState zombieSwitchState, float damage,
            ZombieAttackDetector zombieAttackDetector) : base(animation, zombieSwitchState)
        {
            _attackDetector = zombieAttackDetector;
        }

        public override void Start()
        {
            Animation.SetBool("isAttack", true);
            Debug.Log("attack started");
        }

        public override void Stop()
        {
            Animation.SetBool("isAttack", false);
            Debug.Log("attack ended");
        }

        public override async void Action()
        {
            while (true)
            {
                await Task.Delay(1500);
                if (!ReferenceEquals(_attackDetector.PlayerTarget, null))
                {
                    _attackDetector.PlayerTarget.ApplyDamage(_damage);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
