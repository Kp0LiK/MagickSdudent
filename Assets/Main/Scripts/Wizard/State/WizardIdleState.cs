using Main.Scripts.Wizard_s_Scripts.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Scripts.Wizard.State
{
    public class WizardIdleState : BaseWizardState
    {
        private readonly MagickStudent _playerInputAction;
        public WizardIdleState(Animator animation, IWizardStateSwitch wizardStateSwitch) : base(animation,
            wizardStateSwitch)
        {
            _playerInputAction = new MagickStudent();
        }

        public override void Start()
        {
            _playerInputAction.Player.Move.started += OnMove;

            _playerInputAction.Enable();
        }

        public override void Stop()
        {
            _playerInputAction.Player.Move.started -= OnMove;
            _playerInputAction.Disable();
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