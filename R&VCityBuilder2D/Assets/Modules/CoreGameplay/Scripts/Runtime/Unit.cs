using System;
using Modules.EntitySystem.Scripts.Runtime;
using Modules.HealthSystem.Scripts.Runtime;
using UnityEngine;

namespace Modules.CoreGameplay.Scripts.Runtime
{
    public class Unit : MonoBehaviour, ITarget
    {
        public Entities Entity;
        [SerializeField] private BuildingTypeHolder _buildingTypeHolder;
        
        private HealthUnit _healthUnit;
        private void Awake()
        {
            _buildingTypeHolder ??= GetComponent<BuildingTypeHolder>(); 
            _healthUnit = GetComponent<HealthUnit>();
            
            _healthUnit.SetMaxHealth(_buildingTypeHolder.BuildingType.MaxHealthAmount, true);
            _healthUnit.OnDied += DieBehaviour;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                _healthUnit.Damage(1);
            }
        }
        private void DieBehaviour(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }

        public Entities EntityType => Entity;
        
        public Transform GetTransform()
        {
            return this.transform;
        }

        public void UnitDamaged(int damage)
        {
            _healthUnit.Damage(damage);
        }
    }
}