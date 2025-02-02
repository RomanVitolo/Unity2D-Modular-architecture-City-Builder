using UnityEngine;

namespace Modules.EntitySystem.Scripts.Runtime
{
    public abstract class Entity : MonoBehaviour
    {
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
                FindTargets();
            }
        }
        
        protected virtual void FindTargets()
        {
            var targetMaxRadius = 10f;
            var colliderArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

            foreach (var targetCollider in colliderArray)
            {
                var getTarget = targetCollider.GetComponent<IEnemyTarget>();
                if (getTarget != null)
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