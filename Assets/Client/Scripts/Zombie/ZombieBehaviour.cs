using System.Collections.Generic;
using System.Linq;
using Client.Scripts.Interfaces;
using Client.Scripts.Zombie.States;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Client.Scripts.Zombie
{
    [SelectionBase]
    public class ZombieBehaviour : MonoBehaviour, IZombieSwitchState, IDamageable
    {
        [SerializeField] private AudioSource _audio;
        [SerializeField] private EntityConfig _config;
        private Animator _animator;
        private NavMeshAgent _meshAgent;
        private ZombieAttackDetector _zombieAttackDetector;
        private ZombiePlayerDetector _zombiePlayerDetector;

        private List<BaseZombieState> _states;
        private BaseZombieState _currentState;

        public EntityConfig Config => _config;
        public event UnityAction <float> HealthChanged;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _meshAgent = GetComponent<NavMeshAgent>();
            _zombieAttackDetector = GetComponentInChildren<ZombieAttackDetector>();
            _zombiePlayerDetector = GetComponentInChildren<ZombiePlayerDetector>();
            _states = new List<BaseZombieState>
            {
                new ZombieIdleState(_animator, this),
                new ZombieWalkState(_animator, this, transform, _meshAgent, _zombiePlayerDetector),
                new ZombieAttackState(_animator, this, _config, _zombieAttackDetector),
                new ZombieDeadState(_animator, this, _zombieAttackDetector, _zombiePlayerDetector, _meshAgent)
            };

            _currentState = _states[0];
            _currentState.Start();
            _currentState.Action();
        }

        private void OnEnable()
        {
            _zombiePlayerDetector.Entered += OnEntered;
            _zombiePlayerDetector.DetectExited += OnDetectExited;

            _zombieAttackDetector.Entered += OnZombieAttackDetect;
            _zombieAttackDetector.DetectExited += OnAttackDetectExited;
        }
        

        private void OnDisable()
        {
            _zombiePlayerDetector.Entered -= OnEntered;
            _zombiePlayerDetector.DetectExited -= OnDetectExited;
            
            _zombieAttackDetector.Entered -= OnZombieAttackDetect;
            _zombieAttackDetector.DetectExited -= OnAttackDetectExited;
        }

        private void OnDestroy() => _currentState.Stop();

        private void OnEntered()
        {
            SwitchState<ZombieWalkState>();
        }

        private void OnDetectExited()
        {
            SwitchState<ZombieIdleState>();
        }

        private void OnZombieAttackDetect()
        {
            SwitchState<ZombieAttackState>();
        }

        private void OnAttackDetectExited()
        {
            SwitchState<ZombieWalkState>();
        }


        private void Update()
        {
            switch (_currentState)
            {
                case ZombieIdleState _:
                    _currentState.Action();
                    break;
            }
        }

        public void SwitchState<T>() where T : BaseZombieState
        {
            var state = _states.FirstOrDefault(p => p is T);
            _currentState.Stop();
            _currentState = state;

            if (ReferenceEquals(_currentState, null))
                return;

            _currentState.Start();
            _currentState.Action();
        }

        public void ApplyDamage(float damage)
        {
            _config.Health -= damage;

            if (_config.Health <= 0)
            {
                _audio.Play();
                _config.Health = 0;
                _config.IsDied = true;
            }

            if (_config.IsDied)
            {
                SwitchState<ZombieDeadState>();
            }
            
            HealthChanged?.Invoke(_config.Health);
        }
    }
}