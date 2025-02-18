using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.EntitySystem.Scripts.Runtime
{
    public class Entity_Enemy : Entity, ITarget
    {
        public Entities Entity;
        public Entities EntityType => Entity;
        
        public event Action<ITarget> OnTargetDestroyed;
        
        [SerializeField] private Transform _mainBuildUnitTarget;
        
        private Rigidbody2D _rb;
        private EnemyPool enemyPool;
        private void Start()
        {
            enemyPool ??= FindAnyObjectByType<EnemyPool>();
            _rb = GetComponent<Rigidbody2D>();
            
            waitForTargetTimer = Random.Range(0f, waitForTargetTimerMax);
        }

        protected override void Update()
        {
            HandleMovement();
            base.Update();
        }

        private void OnDisable()
        { 
            OnTargetDestroyed?.Invoke(this);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var target = other.gameObject.GetComponent<ITarget>();
            if (target != null)
            {
                target.UnitDamaged(10);
                if (this != null)
                    enemyPool.ReturnObject(this);
            }
        }

        protected override void FindTargets(Entities entityType, LayerMask layerMask)
        {
            base.FindTargets(entityType, _targetLayer);
            
            unitTarget ??= _mainBuildUnitTarget.gameObject.GetComponent<ITarget>();
        }

        private void HandleMovement()
        {
            if (unitTarget != null)
            {
                var moveDir = (unitTarget.GetTransform().position - transform.position).normalized;

                var moveSpeed = 6f;
                _rb.linearVelocity = moveDir * moveSpeed ; 
            }
            else
                _rb.linearVelocity = Vector2.zero;
        }

        public Transform GetTransform()
        {
            if (this != null) return this.transform;
            OnTargetDestroyed?.Invoke(this);
            return null;
        } 
        public void UnitDamaged(int damage)
        {
            Debug.Log("Do Damage");
        }
    }
}