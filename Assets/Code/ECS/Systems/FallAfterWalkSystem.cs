
using Asserts.Code;
using Assets.Code.ECS.Components;
using Assets.Code.Configs;
using Assets.Code.Systems.Animation;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.Systems
{
    public class FallAfterWalkSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            foreach (var unitEntity in _units.Value)
            {
                var unitAvatar = _units.Pools.Inc1.Get(unitEntity).Avatar;
                var animationTrack = _units.Pools.Inc2.Get(unitEntity).Trak;

                if (!unitAvatar.IsGrounded &&
                    animationTrack != Configs.AnimationTrack.jump &&
                    animationTrack != Configs.AnimationTrack.death &&
                    animationTrack != Configs.AnimationTrack.hurt
                    )
                {
                    _animationService.Value.StartAnimation(
                                   unitEntity, AnimationTrack.idle, 
                                   false, Identifiers.UnitAnimationSpeed);
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
