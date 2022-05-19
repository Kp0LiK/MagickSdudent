using System.Threading.Tasks;
using UnityEngine;

namespace Client.Scripts.Wizard.State
{
    public class WizardHealthReceiveState : BaseWizardState
    {
        private static readonly int IsPick = Animator.StringToHash("isPick");
        private WizardBehaviour _behaviour;

        public WizardHealthReceiveState(Animator animation, IWizardStateSwitch wizardStateSwitch,
            WizardBehaviour behaviour) : base(animation, wizardStateSwitch)
        {
            _behaviour = behaviour;
        }

        public override async void Start()
        {
            Animation.SetBool(IsPick, true);
            await Task.Delay(2000);
            _behaviour.UpdateHealth();
            WizardStateSwitch.SwitchState<WizardIdleState>();
        }

        public override void Stop()
        {
            Animation.SetBool(IsPick, false);
        }

        public override void Action()
        {
        }
    }
}