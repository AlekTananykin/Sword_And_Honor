using Assets.Code.ECS.Components;
using Assets.Code.ECS.Teleport;
using Assets.Code.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.ECS.Systems
{
    sealed class TeleportMoveSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            foreach (var moveEntity in _teleportMove.Value)
            {
                ref var teleportMove = ref _teleportMove.Pools.Inc1.Get(moveEntity);
                ref var transform = ref _teleportMove.Pools.Inc2.Get(moveEntity);

                transform.Transform.position += 
                    teleportMove.Velocity * _timeService.Value.DeltaTime;
            }
        }

        private EcsFilterInject<
            Inc<TeleportMoveComonent, TransformComponent>> 
            _teleportMove = default;

        private readonly EcsCustomInject<TimeService> _timeService = default;
    }
}
