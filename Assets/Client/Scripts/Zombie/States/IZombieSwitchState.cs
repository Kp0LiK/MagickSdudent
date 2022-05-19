namespace Client.Scripts.Zombie.States
{
    public interface IZombieSwitchState
    {
        void SwitchState<T>() where T : BaseZombieState;
    }
}
