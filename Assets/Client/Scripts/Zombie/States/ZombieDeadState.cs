using Client.Scripts.Wizard;
using UnityEngine;
using UnityEngine.AI;

namespace Client.Scripts.Zombie.States
{
    public class ZombieDeadState : BaseZombieState
    {
        private static readonly int IsDeath = Animator.StringToHash("isDeath");

        private ZombieAttackDetector _zombieDetector;
        private ZombiePlayerDetector _playerDetector;
        private NavMeshAgent _meshAgent;

        public ZombieDeadState(Animator animation, IZombieSwitchState zombieSwitchState,
            ZombieAttackDetector zombieDetector,
            ZombiePlayerDetector playerDetector, NavMeshAgent navMeshAgent) : base(animation, zombieSwitchState)
        {
            _zombieDetector = zombieDetector;
            _playerDetector = playerDetector;
            _meshAgent = navMeshAgent;
        }

        public override void Start()
        {
            Animation.SetBool(IsDeath, true);
            _playerDetector.enabled = false;
            _zombieDetector.enabled = false;
            _meshAgent.enabled = false;
            Object.Destroy(_meshAgent.gameObject, 5);
        }

        public override void Stop()
        {
           Animation.SetBool(IsDeath, false);
        }

        public override void Action()
        {
           
        }
    }
}
