using UnityEngine;
using UnityEngine.Serialization;

public enum Entities
{
    Building,
    Enemy
}

namespace Modules.EntitySystem.Scripts.Runtime
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected Entities _entityTarget;
        
        protected Transform target;
        protected float waitForTargetTimer;
        protected float waitForTargetTimerMax = 0.2f;

        protected virtual void Update()
        {
            HandleTargeting();
        }

        private void HandleTargeting()
        {
            waitForTargetTimer -= Time.deltaTime;
            if (waitForTargetTimer < 0)
            {
                waitForTargetTimer += waitForTargetTimerMax;
                FindTargets(_entityTarget);
            }
        }
        
        protected virtual void FindTargets(Entities entityType)
        {
            var targetMaxRadius = 15f;
            var colliderArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

            foreach (var targetCollider in colliderArray)
            {
                var getTarget = targetCollider.GetComponent<ITarget>();
                if (getTarget != null && getTarget.EntityType == entityType)
                {
                    if (target == null) target = getTarget.GetTransform();
                    else
                    {
                        if (Vector3.Distance(transform.position, getTarget.GetTransform().position) <
                            Vector3.Distance(transform.position, target.position))
                        {
                            target = getTarget.GetTransform();
                        }
                    }
                }
            }
        }
    }
}