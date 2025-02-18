using System;
using Modules.CoreGameplay.Scripts.Runtime.BuildingSystem;
using Modules.EntitySystem.Scripts.Runtime;
using Modules.HealthSystem.Scripts.Runtime;
using UnityEngine;

namespace Modules.CoreGameplay.Scripts.Runtime
{
    public class Unit : MonoBehaviour, ITarget
    {
        public Entities Entity;
        
        [SerializeField] private BuildingTypeHolder _buildingTypeHolder;
        [SerializeField] private bool _setHardcodeAmount;
        
         private HealthUnit _healthUnit;
         
        private void Awake()
        {
            _buildingTypeHolder ??= GetComponent<BuildingTypeHolder>(); 
            _healthUnit = GetComponent<HealthUnit>();

            if (!_setHardcodeAmount)
                _healthUnit.SetMaxHealth(_buildingTypeHolder.BuildingType.MaxHealthAmount, true);
            else
                _healthUnit.SetMaxHealth(100, true);
            
            _healthUnit.OnDied += DieBehaviour;
        }
        
        private void DieBehaviour(object sender, EventArgs e) => Destroy(gameObject);

        public Entities EntityType => Entity;
        
        public Transform GetTransform() => this.transform;

        public void UnitDamaged(int damage) => _healthUnit.Damage(damage);
        public event Action<ITarget> OnTargetDestroyed;
    }
}