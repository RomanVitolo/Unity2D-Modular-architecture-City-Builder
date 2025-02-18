using System;
using UnityEngine;

namespace Modules.EntitySystem.Scripts.Runtime.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        private ITarget enemyTarget;
        private Vector3 lastMoveDirection;
        private float timeToDie = 2f;

        public void SetTarget(ITarget target) => enemyTarget = target;

        private ProjectilePool projectilePool;

        private void Awake()
        {
            projectilePool ??= FindAnyObjectByType<ProjectilePool>();
        }

        private void Update()
        {
            try
            {
                if (enemyTarget == null || enemyTarget.GetTransform() == null)
                {
                    projectilePool.ReturnObject(this);
                    return;
                }

                Vector3 moveDir = (enemyTarget.GetTransform().position - transform.position).normalized;
                float moveSpeed = 5f;
                transform.position += moveDir * (moveSpeed * Time.deltaTime);
        
                transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(moveDir));
        
                timeToDie -= Time.deltaTime;
                if (timeToDie < 0f)
                {
                    projectilePool.ReturnObject(this);
                }
            }
            catch (MissingReferenceException)
            {
                projectilePool.ReturnObject(this);
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var obtainTarget = other.GetComponent<ITarget>();
            if (obtainTarget != null)
            {
                obtainTarget.UnitDamaged(10);
            }
            
            FindAnyObjectByType<ProjectilePool>().ReturnObject(this);
        }

        private float GetAngleFromVector(Vector3 vector)
        {
            float radians = Mathf.Atan2(vector.y, vector.x);
            float degrees = radians * Mathf.Rad2Deg;
            return degrees;
        }   
    }
}