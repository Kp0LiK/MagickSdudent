using Client.Scripts.Wizard.State;

namespace Client.Scripts.Wizard.State
{
    public interface IWizardStateSwitch
    {
        void SwitchState<T>() where T : BaseWizardState;
    }
}