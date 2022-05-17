using System.Collections.Generic;
using System.Linq;
using Client.Scripts.Interfaces;
using Client.Scripts.Wizard.State;
using UnityEngine;
using UnityEngine.AI;

namespace Client.Scripts.Wizard
{
    [SelectionBase]
    public class WizardBehaviour : MonoBehaviour, IWizardStateSwitch, IDamageable
    {
        [SerializeField] private EntityConfig _config;
        private Animator _animator;
        private List<BaseWizardState> _states;
        private BaseWizardState _currentState;
        private NavMeshAgent  _meshAgent;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _meshAgent = GetComponent<NavMeshAgent>();

            _states = new List<BaseWizardState>
            {
                new WizardIdleState(_animator, this),
                new WizardRunState(_animator, this, transform, _meshAgent)
            };

            _currentState = _states[0];
            _currentState.Start();
            _currentState.Action();
        }

        private void Update()
        {
            switch (_currentState)
            {
                case WizardIdleState _:
                    _currentState.Action();
                    break;
            }
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
                return;
            }

            if (_config.IsDied)
            {
                //Todo: Make Died State
            }

            _config.Health -= damage;
        }
    }
}