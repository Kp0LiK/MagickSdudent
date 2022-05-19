using System.Collections.Generic;
using Client.Scripts.Zombie;
using UnityEngine;
using UnityEngine.Events;

namespace Client.Scripts.Wizard
{
    public class PlayerAttackDetector : MonoBehaviour
    {
        public event UnityAction Entered;
        public event UnityAction DetectExited;
        public List<ZombieBehaviour> Zombies { get; private set; }
        

        private void Awake()
        {
            Zombies = new List<ZombieBehaviour>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ZombieBehaviour zombieBehaviour))
            {
                if (zombieBehaviour.Config.IsDied)
                    return;

                Debug.Log("Zombie here");
                Zombies.Add(zombieBehaviour);
                Entered?.Invoke();
            }  
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ZombieBehaviour zombieBehaviour))
            {
                if (Zombies.Contains(zombieBehaviour))
                {
                    Debug.Log("Zombie isn't here");
                    Zombies.Remove(zombieBehaviour);
                }
                DetectExited?.Invoke();
            }  
        }
    }
}
