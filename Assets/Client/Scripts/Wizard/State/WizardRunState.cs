using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Client.Scripts.Wizard.State
{
    public class WizardRunState : BaseWizardState
    {
        private static readonly int Run = Animator.StringToHash("Run");
        private readonly Transform _transform;
        private readonly MagickStudent _playerInputAction;
        private readonly NavMeshAgent _agent;
        
        private InputAction _action;
        private InputAction _ability;

        public WizardRunState(Animator animation, IWizardStateSwitch wizardStateSwitch, Transform transform,
            NavMeshAgent agent) : base(animation,
            wizardStateSwitch)
        {
            _transform = transform;
            _agent = agent;
            _playerInputAction = new MagickStudent();
        }

        public override void Start()
        {
            _action = _playerInputAction.Player.Move;
            _ability = _playerInputAction.Abil.abillity;

            _action.canceled += ActionOnCanceled;
            _ability.started += OnFastRun;
            _ability.canceled += OnFastRunCanceled;

            _playerInputAction.Player.Enable();
            _playerInputAction.Abil.Enable();
            Animation.SetFloat(Run, 0.5f);
        }

        public override void Stop()
        {
            _action.canceled -= ActionOnCanceled;
            _ability.started -= OnFastRun;
            _ability.canceled -= OnFastRunCanceled;

            _playerInputAction.Player.Disable();
            _playerInputAction.Abil.Disable();
            Animation.SetFloat(Run, 0);
        }

        private void ActionOnCanceled(InputAction.CallbackContext obj)
        {
            WizardStateSwitch.SwitchState<WizardIdleState>();
        }

        private void OnFastRun(InputAction.CallbackContext obj)
        {
            _agent.speed = 5f;
            Animation.SetFloat(Run, 1f);
        }

        private void OnFastRunCanceled(InputAction.CallbackContext obj)
        {
            _agent.speed = 2.5f;
            Animation.SetFloat(Run, 0.5f);
        }

        public override async void Action()
        {
            while (true)
            {
                await Task.Delay(1);
                var direction = _action.ReadValue<Vector2>();
                if (direction.x != 0 || direction.y != 0)
                {
                    var targetDirection = new Vector3(direction.x, 0, direction.y);

                    if (_agent.isOnNavMesh)
                        _agent.SetDestination(_transform.position + targetDirection);
                    else
                        return;
                }
            }
        }
    }
}