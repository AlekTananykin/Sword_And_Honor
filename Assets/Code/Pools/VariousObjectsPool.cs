using Assets.Code.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Pools
{
    public sealed class VariousObjectsPool: IVariousObjectsPool
    {
        public VariousObjectsPool(IGameObjectFabric fabric)
        {
            _fabric = fabric;
        }

        public GameObject GetGameObject(string prefabPath)
        {
            if (!_poolsStorage.TryGetValue(prefabPath, out SimplePool pool))
            {
                pool = new SimplePool(_fabric, prefabPath);
                _poolsStorage.Add(prefabPath, pool);
            }

            var instance = pool.GetGameObject();
            instance.name = prefabPath;
            _objectPoolMap.Add(instance, pool);

            return instance;
        }

        public void Intake(ref GameObject item)
        {
            if (!_objectPoolMap.TryGetValue(item, out SimplePool pool))
            {
                Debug.LogError(
                    "Various ObjectsPool.Intake>> wrong intaking object");
            }
            _objectPoolMap.Remove(item);
            pool.Intake(ref item);
        }

        private IDictionary<string, SimplePool> _poolsStorage = 
            new Dictionary<string, SimplePool>();

        private IDictionary<GameObject, SimplePool> _objectPoolMap =
            new Dictionary<GameObject, SimplePool>();

        private IGameObjectFabric _fabric;
    }
}
