using Asserts.Code;
using Assets.Code.Systems.Animation;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.ECS.Animation
{
    sealed class SwitchSleepAnimationToIdleSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {

            foreach (var entity in _sleepFilter.Value)
            {
                _animationService.Value.StartAnimation(entity,
                    Configs.AnimationTrack.idle, true, 
                    Identifiers.UnitAnimationSpeed);

                _sleepFilter.Pools.Inc1.Del(entity);
                _sleepFilter.Pools.Inc2.Del(entity);
            }
        }

        private EcsFilterInject<Inc<IsSleep, IsNeedSwitchToIdle>> 
            _sleepFilter = default;

        private EcsCustomInject<ControlAnimationService>
            _animationService = default;
    }
}
