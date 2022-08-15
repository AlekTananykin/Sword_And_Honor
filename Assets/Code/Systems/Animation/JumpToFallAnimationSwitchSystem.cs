using Assets.Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.Systems.Animation
{
    class JumpToFallAnimationSwitchSystem : IEcsRunSystem
    {
        private EcsCustomInject<ControlAnimationService> _unimationService = default;
        private EcsFilterInject<Inc<JumpComponent, UnitComponent>> _jumpUnits = default;

        private EcsPoolInject<UnitComponent> _unitPool = default;
        private EcsPoolInject<JumpComponent> _JumpPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var unitEntity in _jumpUnits.Value)
            {
                ref var unit = ref _unitPool.Value.Get(unitEntity);

                if (unit.RigidBody.velocity.y < 0)
                {
                    _unimationService.Value.StartAnimation(unitEntity, 
                        Configs.AnimationTrack.fall, false, 
                        Asserts.Code.Identifiers.UnitAnimationSpeed);

                    _JumpPool.Value.Del(unitEntity);
                }
            }
        }
    }
}
