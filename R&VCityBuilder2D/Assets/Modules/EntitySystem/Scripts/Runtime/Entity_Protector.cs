using UnityEngine;

namespace Modules.EntitySystem.Scripts.Runtime
{
    public class Entity_Protector : Entity
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            var enemyTarget = other.gameObject.GetComponent<IEnemyTarget>();
            if (target != null)
            {
                enemyTarget.UnitDamaged(10);
                Destroy(gameObject);
            }
        }
    }
}