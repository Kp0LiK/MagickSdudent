using Client.Scripts.Zombie;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHeathViewer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private ZombieBehaviour _zombieBehaviour;

    private void Awake()
    {
        _zombieBehaviour = GetComponentInParent<ZombieBehaviour>();
    }

    private void OnEnable()
    {
        _zombieBehaviour.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _zombieBehaviour.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float health)
    {
        _slider.DOValue(health, 0.5f);
    }
}
