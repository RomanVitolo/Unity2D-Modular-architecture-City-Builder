using Modules.PoolSystem.Runtime.Scripts;

namespace Modules.EntitySystem.Scripts.Runtime.Projectiles
{
    public class ProjectilePool : BaseObjectPool<Projectile>
    {
        protected override void Awake() => objectPool = 
            new ObjectPool<Projectile>(_prefabType, _initialPoolSize, _objectParent);

        public override void ReturnObject(Projectile obj) => base.ReturnObject(obj);
    }
}