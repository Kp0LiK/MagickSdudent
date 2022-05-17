using Client.Scripts.Wizard;
using UnityEngine;
using UnityEngine.Events;

namespace Client.Scripts.Zombie
{
    public class ZombieAttackDetector : MonoBehaviour
    {
        public event UnityAction Entered;
        public event UnityAction DetectExited;

        public WizardBehaviour PlayerTarget { get; private set; }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out WizardBehaviour wizardBehaviour))
            {
                PlayerTarget = wizardBehaviour;
                Entered?.Invoke();
            }  
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out WizardBehaviour wizard))
            {
                DetectExited?.Invoke();
                PlayerTarget = null;
            }  
        }
    }
}