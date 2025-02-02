using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.EntitySystem.Scripts.Runtime
{
    public class Entity_Enemy : Entity, IEnemyTarget
    {
        public static Entity_Enemy Create(Vector3 position)
        {
            var enemyPrefab = Resources.Load<Transform>("EnemyEntity");
            var newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);

            var enemy = newEnemy.GetComponent<Entity_Enemy>();
            return enemy;
        }
        
        [SerializeField] private Transform _target;
        [SerializeField] private Transform _mainBuildUnitTarget;
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            
            waitForTargetTimer = Random.Range(0f, waitForTargetTimerMax);
        }

        private void Update()
        {
            HandleMovement();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var target = other.gameObject.GetComponent<IBuildingTarget>();
            if (target != null)
            {
                target.UnitDamaged(10);
                Destroy(gameObject);
            }
        }
        protected override void FindTargets()
        {
            var targetMaxRadius = 10f;
            var colliderArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

            foreach (var targetCollider in colliderArray)
            {
                var getTarget = targetCollider.GetComponent<IBuildingTarget>();
                if (getTarget != null)
                {
                    if (_target == null) _target = getTarget.GetTransform();
                    else
                    {
                        if (Vector3.Distance(transform.position, getTarget.GetTransform().position) <
                            Vector3.Distance(transform.position, _target.position))
                        {
                            _target = getTarget.GetTransform();
                        }
                    }
                }
            }
            _target ??= _mainBuildUnitTarget;
        }

        private void HandleMovement()
        {
            if (_target != null)
            {
                var moveDir = (_target.position - transform.position).normalized;

                var moveSpeed = 6f;
                _rb.linearVelocity = moveDir * moveSpeed ; 
            }
            else
                _rb.linearVelocity = Vector2.zero;
        }

        public Transform GetTransform()
        {
            return this.transform;
        }

        public void UnitDamaged(int damage)
        {
            throw new System.NotImplementedException();
        }
    }
}