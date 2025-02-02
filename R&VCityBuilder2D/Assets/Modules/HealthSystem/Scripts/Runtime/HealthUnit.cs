using System;
using UnityEngine;

namespace Modules.HealthSystem.Scripts.Runtime
{
    public class HealthUnit : MonoBehaviour
    {
        public event EventHandler OnDamaged;
        public event EventHandler OnDied;
        
        private int _maxHealthAmount;
        private int currentHealth;
        private void Awake() => currentHealth = _maxHealthAmount;
        public void Damage(int damageAmount)
        {
            currentHealth -= damageAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, _maxHealthAmount);
            OnDamaged?.Invoke(this, EventArgs.Empty);

            if (IsDead())
                OnDied?.Invoke(this, EventArgs.Empty);
        }
        public bool IsDead() => currentHealth == 0;
        public bool IsFullHealth() => currentHealth == _maxHealthAmount;
        public int GetCurrentHealth() => currentHealth;
        public float GetHealthAmountNormalized() => (float)currentHealth / _maxHealthAmount;
        public void SetMaxHealth(int maxHealthAmount, bool updateHealth)
        {
            _maxHealthAmount = maxHealthAmount;
            if (updateHealth)
                currentHealth = maxHealthAmount;
        }
    }
}