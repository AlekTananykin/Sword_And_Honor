using Asserts.Code;
using Assets.Code.ECS.Components;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.ECS.Death
{
    class DeathSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            foreach (var unitEntity in _healthFilter.Value)
            {
                var health = _healthFilter.Pools.Inc1.Get(unitEntity);
                if (health.Health <= 0)
                {
                    _animationService.Value.StartAnimation(unitEntity,
                        Configs.AnimationTrack.death, false, 
                        Identifiers.UnitAnimationSpeed);

                    _healthFilter.Pools.Inc1.Del(unitEntity);
                    _deadPool.Value.Add(unitEntity);
                }
            }
        }

        private EcsFilterInject<Inc<HealthComponent>, Exc<IsDead>> 
            _healthFilter = default;

        private EcsCustomInject<IControlAnimationService>
            _animationService = default;

        private EcsPoolInject<IsDead> _deadPool = default;
    }
}
