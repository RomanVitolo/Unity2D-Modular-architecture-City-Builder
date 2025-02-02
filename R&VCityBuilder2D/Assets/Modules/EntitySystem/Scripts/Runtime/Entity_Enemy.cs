using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.EntitySystem.Scripts.Runtime
{
    public class Entity_Enemy : Entity, ITarget
    {
        public static Entity_Enemy Create(Vector3 position)
        {
            var enemyPrefab = Resources.Load<Transform>("EnemyEntity");
            var newEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);

            var enemy = newEnemy.GetComponent<Entity_Enemy>();
            return enemy;
        }
        
        [SerializeField] private Transform _mainBuildUnitTarget;
        private Rigidbody2D _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            
            waitForTargetTimer = Random.Range(0f, waitForTargetTimerMax);
        }

        protected override void Update()
        {
            HandleMovement();
            base.Update();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var target = other.gameObject.GetComponent<ITarget>();
            if (target != null)
            {
                target.UnitDamaged(10);
                Destroy(gameObject);
            }
        }

        protected override void FindTargets(Entities entityType)
        {
            base.FindTargets(entityType);
            target ??= _mainBuildUnitTarget;
        }

        private void HandleMovement()
        {
            if (target != null)
            {
                var moveDir = (target.position - transform.position).normalized;

                var moveSpeed = 6f;
                _rb.linearVelocity = moveDir * moveSpeed ; 
            }
            else
                _rb.linearVelocity = Vector2.zero;
        }

        public Entities Entity;
        public Entities EntityType => Entity;

        public Transform GetTransform()
        {
            return this.transform;
        }

        public void UnitDamaged(int damage)
        {
            Debug.Log("Do Damage");
        }
    }
}