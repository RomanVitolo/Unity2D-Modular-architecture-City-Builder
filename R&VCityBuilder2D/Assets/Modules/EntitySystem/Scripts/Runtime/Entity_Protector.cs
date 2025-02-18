using System;
using Modules.EntitySystem.Scripts.Runtime.Projectiles;
using UnityEngine;

namespace Modules.EntitySystem.Scripts.Runtime
{
    public class Entity_Protector : Entity
    {
        [SerializeField] private ProjectilePool _projectilePool;
        [SerializeField] private Transform _projectileSpawnPoint;
        [SerializeField] private float shootTimerMax;
        private float shootTimer;

        protected override void Update()
        {
            HandleTargeting();
            HandleShoot();
        }

        private void HandleShoot()
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                shootTimer += shootTimerMax;

                try
                {
                    if (unitTarget == null || ReferenceEquals(unitTarget, null))
                    {
                        unitTarget = null;
                        return;
                    }
                   
                    var newProjectile = _projectilePool.GetObject();
                    newProjectile.gameObject.transform.SetPositionAndRotation(_projectileSpawnPoint.position, Quaternion.identity);
                    var component = newProjectile.GetComponent<Projectile>();
                    component.SetTarget(unitTarget);
                }
                catch (MissingReferenceException)
                {
                    unitTarget = null;
                }
            }
        }
        
        protected override void FindTargets(Entities entityType, LayerMask layerMask)
        {
            var targetMaxRadius = 15f;
            var colliderArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius, layerMask);
    
            foreach (var targetCollider in colliderArray)
            {
                var getTarget = targetCollider.GetComponent<ITarget>();
                if (getTarget != null && getTarget.EntityType == entityType)
                {
                    if (unitTarget == null)
                    {
                        unitTarget = getTarget;
                        unitTarget.OnTargetDestroyed += OnTargetDestroyed; // Suscribirse al evento
                    }
                    else
                    {
                        if (unitTarget != null && !ReferenceEquals(unitTarget, null))
                        {
                            if (Vector3.Distance(transform.position, getTarget.GetTransform().position) <
                                Vector3.Distance(transform.position, unitTarget.GetTransform().position))
                            {
                                unitTarget.OnTargetDestroyed -= OnTargetDestroyed; 
                                unitTarget = getTarget;
                                unitTarget.OnTargetDestroyed += OnTargetDestroyed; 
                            }
                        }
                    }
                }
            }
        }
        
        private void OnTargetDestroyed(ITarget target)
        {
            if (unitTarget == target)
            {
                unitTarget = null;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var enemyTarget = other.gameObject.GetComponent<ITarget>();
            if (unitTarget != null && enemyTarget.EntityType == Entities.Enemy)
            {
                enemyTarget.UnitDamaged(10);
            }
        }
    }
}