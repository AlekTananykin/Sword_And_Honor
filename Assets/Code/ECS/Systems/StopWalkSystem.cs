using Asserts.Code;
using Assets.Code.ECS.Components;
using Assets.Code.Configs;
using Assets.Code.Systems.Animation;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.Systems
{
    class StopWalkSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _units.Value)
            {
                ref var unit = ref _units.Pools.Inc1.Get(entity);

                if (0.0f == unit.RigidBody.velocity.magnitude)
                {
                    var currentTrack =
                        _units.Pools.Inc2.Get(entity).Trak;

                    if (AnimationTrack.walk == currentTrack)
                    {
                        _animationService.Value.StartAnimation(
                            entity, AnimationTrack.idle, true, 
                            Identifiers.UnitAnimationSpeed);
                    }
                }
            }
        }
        
        private EcsFilterInject<
            Inc<UnitComponent, AnimationContextComponent>, 
            Exc<IsActive>> _units = default;

        private EcsCustomInject<ControlAnimationService> 
            _animationService = default;
    }
}
