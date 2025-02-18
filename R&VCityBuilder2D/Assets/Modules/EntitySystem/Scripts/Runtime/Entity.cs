using Modules.PoolSystem.Runtime.Scripts;
using UnityEngine;

namespace Modules.EntitySystem.Scripts.Runtime
{
    public enum Entities
    {
        Building,
        Enemy
    }

    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected Entities _entityTarget;
        [SerializeField] protected LayerMask _targetLayer;
        
        protected ITarget unitTarget;
        protected float waitForTargetTimer;
        protected float waitForTargetTimerMax = 0.2f;

        protected virtual void Update()
        {
            HandleTargeting();
        }

        protected void HandleTargeting()
        {
            waitForTargetTimer -= Time.deltaTime;
            if (waitForTargetTimer < 0)
            {
                waitForTargetTimer += waitForTargetTimerMax;
                FindTargets(_entityTarget, _targetLayer);
            }
        }
        
        protected virtual void FindTargets(Entities entityType, LayerMask layerMask)
        {
            var targetMaxRadius = 30f;
            var colliderArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius, layerMask);
    
            foreach (var targetCollider in colliderArray)
            {
                var getTarget = targetCollider.GetComponent<ITarget>();
                if (getTarget != null && getTarget.EntityType == entityType)
                {
                    if (unitTarget == null) unitTarget = getTarget;
                    else
                    {
                        if (unitTarget != null && unitTarget.GetTransform() != null)
                        {
                            if (Vector3.Distance(transform.position, getTarget.GetTransform().position) <
                                Vector3.Distance(transform.position, unitTarget.GetTransform().position))
                            {
                                unitTarget = getTarget;
                            }
                        }
                    }
                }
            }
        }
    }
}