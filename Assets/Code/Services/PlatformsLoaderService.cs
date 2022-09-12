
using Assets.Code.ECS.Components;
using Assets.Code.ECS.Components.Init;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Code.Services
{
    public class PlatformsLoaderService
    {
        public PlatformsLoaderService(string pathToConfig, IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var isPlatformPool = world.GetPool<IsPlatform>();
            var gameObjectDataPool = world.GetPool<GameObjectData>();

            var platformsConfig = Resources.Load<PlatformsLocationConfig>(pathToConfig);

            for (int i = 0; i < platformsConfig.Count; ++i)
            {
                var platformData = platformsConfig[i];
                int entity = world.NewEntity();

                isPlatformPool.Add(entity);
                ref var gameObjectData = ref  gameObjectDataPool.Add(entity);
                gameObjectData = platformData;
            }
        }
    }
}
