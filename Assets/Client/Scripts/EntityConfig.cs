using System;
using UnityEngine;
using UnityEngine.Events;

namespace Client.Scripts
{
    [Serializable]
    public class EntityConfig
    {
        [field: SerializeField] public float Health { get; set; }
        [field: SerializeField] public float Damage { get; set; }
        
        public bool IsDied { get; set; }
    }
}