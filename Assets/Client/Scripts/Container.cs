using System;
using Client.Scripts.Wizard;
using UnityEngine;

namespace Client.Scripts
{
    public class Container : MonoBehaviour
    {
        public static Container Instance { get; private set; }
        public WizardBehaviour Player { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
            
            Player = FindObjectOfType<WizardBehaviour>();
        }
    }
}