using UnityEngine;

namespace Modules.EntitySystem.Scripts.Runtime
{
    public interface IBuildingTarget
    {
        public Transform GetTransform();
        public void UnitDamaged(int damage);
    }
}