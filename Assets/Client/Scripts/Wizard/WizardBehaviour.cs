using System.Collections.Generic;
using System.Linq;
using Client.Scripts.Interfaces;
using Client.Scripts.Wizard.State;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace Client.Scripts.Wizard
{
    [SelectionBase]
    public class WizardBehaviour : MonoBehaviour, IWizardStateSwitch, IDamageable
    {
        [SerializeField] private EntityConfig _config;
        [SerializeField] private AudioSource _audio;
        private Animator _animator;
        private List<BaseWizardState> _states;
        private BaseWizardState _currentState;
        private NavMeshAgent  _meshAgent;
        private PlayerAttackDetector _detector;

        public EntityConfig Config => _config;

        public event UnityAction<float> HealthChanged;

        private void Awake()
        {
            _detector = GetComponentInChildren<PlayerAttackDetector>();
            _animator = GetComponent<Animator>();
            _meshAgent = GetComponent<NavMeshAgent>();

            _states = new List<BaseWizardState>
            {
                new WizardIdleState(_animator, this),
                new WizardRunState(_animator, this, transform, _meshAgent),
                new WizardAttackState(_animator, this, _detector, _config.Damage, _audio),
                new WizardHealthReceiveState(_animator, this, this),
                new WizardDeadState(_animator, this)
            };

            _currentState = _states[0];
            _currentState.Start();
            _currentState.Action();
        }

        public void SwitchState<T>() where T : BaseWizardState
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
            if (_config.Health <= 0)
            {
                _config.Health = 0;
                _config.IsDied = true;
            }

            if (_config.IsDied)
            {
                SwitchState<WizardDeadState>();
            }
            else
            {
                _config.Health -= damage;
            }

            UpdateHealth();
        }

        public void UpdateHealth() => HealthChanged?.Invoke(_config.Health);

        public void HealthAdd(float health)
        {
            if (!_config.IsDied && _config.Health < 100)
            {
                _config.Health += health;
            }
        }
    }
}