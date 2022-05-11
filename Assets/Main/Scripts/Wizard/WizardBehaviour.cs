using System.Collections.Generic;
using System.Linq;
using Main.Scripts.Wizard.State;
using Main.Scripts.Wizard_s_Scripts.State;
using UnityEngine;
using UnityEngine.AI;

namespace Main.Scripts.Wizard
{
    public class WizardBehaviour : MonoBehaviour, IWizardStateSwitch
    {
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
    }
}