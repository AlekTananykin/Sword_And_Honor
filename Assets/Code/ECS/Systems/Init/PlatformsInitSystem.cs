using Assets.Code.ECS.Components;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.ECS.Systems.Init
{
    sealed class PlatformsInitSystem: IEcsInitSystem
    {
        public PlatformsInitSystem(string pathToConfig)
        {
            _pathToConfig = pathToConfig;
        }

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var budPool = world.GetPool<Bud>();
            var gameObjectPool = world.GetPool<GameObjectComponent>();

            var platformsConfig =
                Resources.Load<PlatformsLocationConfig>(_pathToConfig);

            for (int i = 0; i < platformsConfig.Count; ++i)
            {
                var platformData = platformsConfig[i];
                int entity = world.NewEntity();

                var platformGameObject = 
                    _pool.Value.GetGameObject(platformData.Path);
                platformGameObject.transform.position = platformData.Position;

                ref var poolBud = ref budPool.Add(entity);
                poolBud = platformData;

                gameObjectPool.Add(entity).Instance = platformGameObject;
            }
        }

        EcsCustomInject<IVariousObjectsPool> _pool = default;
        

        private string _pathToConfig = default;
    }
}
