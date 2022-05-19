using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Client.Scripts.Wizard.State
{
    public class WizardAttackState : BaseWizardState
    {
        private AudioSource _audio;
        private readonly PlayerAttackDetector _detector;
        private float _damage;
        private static readonly int IsAttack = Animator.StringToHash("isAttack");

        public WizardAttackState(Animator animation, IWizardStateSwitch wizardStateSwitch, PlayerAttackDetector detector, float damage, AudioSource audio) 
            : base(animation, wizardStateSwitch)
        {
            _detector = detector;
            _damage = damage;
            _audio = audio;
        }

        public override async void Start()
        {
            _audio.Play();
            Debug.Log("In Attack");
            Animation.SetBool(IsAttack, true);
            if(_detector.Zombies.Count <= 0)
            {
                await Task.Delay(1200);
                WizardStateSwitch.SwitchState<WizardIdleState>();
                return;
            }

            foreach (var zombie in _detector.Zombies)
            {
                zombie.ApplyDamage(_damage);
            }

            await Task.Delay(1200);
            WizardStateSwitch.SwitchState<WizardIdleState>();
        }

        public override void Stop()
        {
            Debug.Log("Out Attack");
            Animation.SetBool(IsAttack, false);
        }

        public override void Action()
        {
        }
    }
}
