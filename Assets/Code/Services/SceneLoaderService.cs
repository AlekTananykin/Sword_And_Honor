
using Assets.Code.ECS.Components;
using Assets.Code.ECS.Components.Init;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Code.Services
{
    public class SceneLoaderService
    {
        public SceneLoaderService(string path, IEcsSystems systems)
        {
            var world = systems.GetWorld();

            var isPlatformPool = world.GetPool<IsPlatform>();
            var gameObjectDataPool = world.GetPool<GameObjectData>();

            var sceneConfig = Resources.Load<SceneConfig>(path);

            for (int i = 0; i < sceneConfig.Count; ++i)
            {
                var platformData = sceneConfig[i];
                int entity = world.NewEntity();

                isPlatformPool.Add(entity);
                ref var gameObjectData = ref  gameObjectDataPool.Add(entity);
                gameObjectData = platformData;
            }
        }
    }
}
