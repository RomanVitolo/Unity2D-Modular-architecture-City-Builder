using System;
using UnityEngine;

namespace Modules.HealthSystem.Scripts.Runtime
{
    public class HealthBar : MonoBehaviour
    
    {
        [SerializeField] private HealthUnit _healthUnit;
        [SerializeField] private Transform _barTransform;
        private void Awake()
        {
            _healthUnit ??= GetComponentInParent<HealthUnit>();
        }

        private void Start()
        {
            _healthUnit.OnDamaged += DamageBehaviour;
            HealthBarVisible();
        }

        private void DamageBehaviour(object sender, EventArgs e)
        {
            UpdateBar();
            HealthBarVisible();
        }

        private void UpdateBar() =>
            _barTransform.localScale = new Vector3(_healthUnit.GetHealthAmountNormalized(), 1, 1);

        private void HealthBarVisible()
        {
            gameObject.SetActive(!_healthUnit.IsFullHealth());
        }
    }
}