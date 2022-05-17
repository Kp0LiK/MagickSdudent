using UnityEngine;

namespace Client.Scripts.Wizard.State
{
    public abstract class BaseWizardState
    {
        protected readonly Animator Animation;
        protected readonly IWizardStateSwitch WizardStateSwitch;

        protected BaseWizardState(Animator animation, IWizardStateSwitch wizardStateSwitch)
        {
            Animation = animation;
            WizardStateSwitch = wizardStateSwitch;
        }

        public abstract void Start();
        public abstract void Stop();
        public abstract void Action();
    }
}