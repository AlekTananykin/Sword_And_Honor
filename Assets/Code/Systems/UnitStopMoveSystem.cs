

using Assets.Code.Components;
using Assets.Code.Components.Commands;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.Systems
{
    public class UnitStopMoveSystem : IEcsRunSystem
    {
        private EcsCustomInject<IControlAnimationService> _animationService;

        private EcsFilterInject<Inc<UnitComponent, StopMoveCommand>>
           _idleUnitFilter = default;

        private EcsPoolInject<StopMoveCommand> _idleCommandPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _idleUnitFilter.Value)
            {
                _animationService.Value.StartAnimation(
                    entity, Configs.AnimationTrack.idle, true, 5.0f);

                _idleCommandPool.Value.Del(entity);
            }
        }
    }
}
