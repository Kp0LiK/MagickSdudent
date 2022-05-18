using Client.Scripts.Wizard;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthViewer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private WizardBehaviour _wizardBehaviour;

    private void Awake()
    {
        _wizardBehaviour = FindObjectOfType<WizardBehaviour>();
    }

    private void OnEnable()
    {
        _wizardBehaviour.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _wizardBehaviour.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float health)
    {
        _slider.DOValue(health, 0.5f);
    }
}