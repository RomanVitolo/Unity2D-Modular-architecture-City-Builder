using Modules.PoolSystem.Runtime.Scripts;

namespace Modules.EntitySystem.Scripts.Runtime
{
    public class EnemyPool : BaseObjectPool<Entity_Enemy>
    {
        protected override void Awake() => objectPool = 
            new ObjectPool<Entity_Enemy>(_prefabType, _initialPoolSize, _objectParent);

        public override void ReturnObject(Entity_Enemy obj) => base.ReturnObject(obj);
    }
}