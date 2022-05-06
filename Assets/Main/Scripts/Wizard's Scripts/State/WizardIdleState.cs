using System.Threading.Tasks;
using UnityEngine;

namespace Main.Scripts.Wizard_s_Scripts.State
{
    public class WizardIdleState : BaseWizardState
    {
        public WizardIdleState(Animator animation, IWizardStateSwitch wizardStateSwitch) : base(animation,
            wizardStateSwitch)
        {
        }

        public override void Start()
        {
            Debug.Log("Idle State");
        }

        public override void Stop()
        {
            Debug.Log("Idle State Stopped");
        }

        public override void Action()
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A)
                || Input.GetKeyDown(KeyCode.D))
            {
                WizardStateSwitch.SwitchState<WizardRunState>();
            }
        }
    }
}