using Assets.Code.ECS.Components;
using Assets.Code.ECS.Components.Init;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.ECS.Systems.Init
{
    sealed class PlatformsInitSystem : IEcsInitSystem
    {
        EcsFilterInject<Inc<GameObjectData, IsPlatform>> _platforms = default;
        EcsCustomInject<IVariousObjectsPool> _pool = default;

        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            foreach (var platformEntity in _platforms.Value)
            {
                var platform = _platforms.Pools.Inc1.Get(platformEntity);

                var platformGameObject = _pool.Value.GetGameObject(platform.Path);
                platformGameObject.transform.position = platform.Position;

                world.DelEntity(platformEntity);
            }
        }
    }
}
