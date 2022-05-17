using System.Threading.Tasks;
using UnityEngine;

namespace Client.Scripts.Zombie.States
{
    public class ZombieAttackState : BaseZombieState
    {
        private readonly ZombieAttackDetector _attackDetector;
        private float _damage;
        private static readonly int IsAttack = Animator.StringToHash("isAttack");


        public ZombieAttackState(Animator animation, IZombieSwitchState zombieSwitchState, float damage,
            ZombieAttackDetector zombieAttackDetector) : base(animation, zombieSwitchState)
        {
            _attackDetector = zombieAttackDetector;
            _damage = damage;
        }

        public override void Start()
        {
            Animation.SetBool(IsAttack, true);
        }

        public override void Stop()
        {
            Animation.SetBool(IsAttack, false);
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
