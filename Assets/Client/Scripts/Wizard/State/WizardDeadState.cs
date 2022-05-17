using UnityEngine;

namespace Client.Scripts.Wizard.State
{
    public class WizardDeadState : BaseWizardState
    {
        private static readonly int IsDie = Animator.StringToHash("isDie");

        public WizardDeadState(Animator animation, IWizardStateSwitch wizardStateSwitch) : base(animation, wizardStateSwitch)
        {
        }

        public override void Start()
        {
            Animation.SetBool(IsDie, true);
        }

        public override void Stop()
        {
            Animation.SetBool(IsDie, false);
        }

        public override void Action()
        {
       
        }
    }
}
