using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Client.Scripts.Zombie.States
{
    public class ZombieWalkState : BaseZombieState
    {
        private readonly NavMeshAgent _meshAgent;
        private readonly ZombiePlayerDetector _detector;
        
        private static readonly int Walk = Animator.StringToHash("Walk");
        private Vector3 _target;

        public ZombieWalkState(Animator animation, IZombieSwitchState zombieSwitchState, Transform transform,
            NavMeshAgent navMeshAgent, ZombiePlayerDetector detector) :
            base(animation, zombieSwitchState)
        {
            _meshAgent = navMeshAgent;
            _detector = detector;
        }

        public override void Start()
        {
            Animation.SetFloat(Walk, 1f);
        }

        public override void Stop()
        {
            Animation.SetFloat(Walk, 0f);
        }

        public override async void Action()
        {
            while (true)
            {
                await Task.Delay(1);

                if (ReferenceEquals(_detector.PlayerTarget, null))
                    return;

                if (ReferenceEquals(_meshAgent, null))
                    return;

                if (_meshAgent.isOnNavMesh)
                {
                    _meshAgent.SetDestination(_detector.PlayerTarget.transform.position);
                }
                else
                {
                    return;
                }
            }
        }
    }
}