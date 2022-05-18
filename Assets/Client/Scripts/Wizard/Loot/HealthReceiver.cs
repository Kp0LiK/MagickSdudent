using System;
using Client.Scripts.Wizard.State;
using UnityEngine;
using UnityEngine.Events;

namespace Client.Scripts.Wizard.Loot
{
    public class HealthReceiver : MonoBehaviour
    {
        [SerializeField] private float _health;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out WizardBehaviour wizardBehaviour))
            {
                if (Math.Abs(wizardBehaviour.Config.Health - 100) < 0.01f){
                    return;
                }
                
                wizardBehaviour.HealthAdd(_health);
                wizardBehaviour.SwitchState<WizardHealthRecieverState>();
                Destroy(gameObject, 1);
            }
        }
    }
}