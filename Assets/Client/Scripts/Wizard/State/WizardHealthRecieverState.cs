using System.Threading.Tasks;
using UnityEngine;

namespace Client.Scripts.Wizard.State
{
    public class WizardHealthRecieverState : BaseWizardState
    {
        private static readonly int IsPick = Animator.StringToHash("isPick");

        public WizardHealthRecieverState(Animator animation, IWizardStateSwitch wizardStateSwitch) : base(animation, wizardStateSwitch)
        {
        }

        public override async void Start()
        {
            Animation.SetBool(IsPick, true);
            await Task.Delay(2000);
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
