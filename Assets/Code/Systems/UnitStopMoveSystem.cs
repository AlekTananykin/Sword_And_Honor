

using Assets.Code.Components;
using Assets.Code.Components.Commands;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.Systems
{
    public class UnitStopMoveSystem : UnitSystemBase, IEcsRunSystem
    {
        private EcsFilterInject<Inc<Unit, StopMoveCommand>>
           _idleUnitFilter = default;

        private EcsPoolInject<StopMoveCommand> _idleCommandPool = default;
        private EcsPoolInject<UnitAnimationComponent> 
            _unitAnimationPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _idleUnitFilter.Value)
            {
                _animationService.StartAnimation(
                    entity, Configs.Track.idle, true, 5.0f);

                _idleCommandPool.Value.Del(entity);
            }
        }
    }
}
