using Assets.Code.ECS.Components;
using Assets.Code.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;

namespace Assets.Code.ECS.Teleport
{
    class TeleportSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var transform = ref _filter.Pools.Inc1.Get(entity);
                ref var moving = ref _filter.Pools.Inc2.Get(entity);

                transform.Transform.position += moving.Velocity * _timeService.Value.DeltaTime;
            }
        }

        EcsCustomInject<TimeService> _timeService;
        EcsFilterInject<Inc<TransformComponent, TeleportMoveComonent>> _filter = default;
    }
}
