namespace Main.Scripts.Wizard_s_Scripts.State
{
    public interface IWizardStateSwitch
    {
        void SwitchState<T>() where T : BaseWizardState;
    }
}