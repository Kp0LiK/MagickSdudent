using UnityEngine;
using UnityEngine.InputSystem;

namespace Client.Scripts.Wizard.State
{
    public class WizardIdleState : BaseWizardState
    {
        private readonly MagickStudent _playerInputAction;
        private InputAction _attack;
        public WizardIdleState(Animator animation, IWizardStateSwitch wizardStateSwitch) : base(animation,
            wizardStateSwitch)
        {
            _playerInputAction = new MagickStudent();
            _attack = new InputAction();
        }

        public override void Start()
        {
            _playerInputAction.Player.Move.started += OnMove;
            _attack = _playerInputAction.Player.Fire;
            
            _attack.started += OnAttack;

            _attack.Enable();
            _playerInputAction.Enable();
        }

        private void OnAttack(InputAction.CallbackContext obj)
        {
            WizardStateSwitch.SwitchState<WizardAttackState>();
        }

        public override void Stop()
        {
            _playerInputAction.Player.Move.started -= OnMove;
            _attack.started -= OnAttack;
            _playerInputAction.Disable();
            _attack.Disable();
        }

        public override void Action()
        {
            
        }
        
        private void OnMove(InputAction.CallbackContext obj)
        {
            WizardStateSwitch.SwitchState<WizardRunState>();
        }
    }
}