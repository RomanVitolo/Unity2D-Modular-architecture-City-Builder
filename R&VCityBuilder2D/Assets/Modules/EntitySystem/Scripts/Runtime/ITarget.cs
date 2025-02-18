using System;
using UnityEngine;

namespace Modules.EntitySystem.Scripts.Runtime
{
    public interface ITarget
    {
        public Entities EntityType { get; }
        public Transform GetTransform();
        public void UnitDamaged(int damage);
        event Action<ITarget> OnTargetDestroyed;
    }
}