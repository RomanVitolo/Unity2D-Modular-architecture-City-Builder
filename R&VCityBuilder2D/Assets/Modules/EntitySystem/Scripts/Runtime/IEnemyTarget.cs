using UnityEngine;

namespace Modules.EntitySystem.Scripts.Runtime
{
    public interface IEnemyTarget
    {
        public Transform GetTransform();
        public void UnitDamaged(int damage);
    }
}