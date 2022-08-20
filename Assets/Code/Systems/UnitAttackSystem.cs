using Assets.Code.Components;
using Assets.Code.Components.Commands;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.Systems
{
    public sealed class UnitAttackSystem : IEcsRunSystem
    {
        private EcsCustomInject<IControlAnimationService> _animationService;

        private EcsFilterInject<Inc<UnitComponent
            , AttackCommand>>
            _attackUnitFilter = default;

        private EcsPoolInject<IsReadyToGetCommandComponent>
           _isReadyToGetCommandPool = default;

        private EcsPoolInject<AttackCommand> _attackCommandPool = default;
        private EcsPoolInject<AttackComponent> _attackPool = default;

        private EcsPoolInject<UnitComponent> _unitsPool = default;

        private EcsPoolInject<HealthComponent> _healthPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _attackUnitFilter.Value)
            {
                if (_isReadyToGetCommandPool.Value.Has(entity))
                {
                    ref var previousContext = ref _isReadyToGetCommandPool.Value.Get(entity);

                    Configs.AnimationTrack nextAnimation;
                    switch (previousContext.Track)
                    {
                        case Configs.AnimationTrack.attack1:
                            nextAnimation = Configs.AnimationTrack.attack2;
                            break;
                        case Configs.AnimationTrack.attack2:
                            nextAnimation = Configs.AnimationTrack.attack3;
                            break;
                        default:
                            nextAnimation = Configs.AnimationTrack.attack1;
                            break;
                    }

                    _animationService.Value.StartAnimation(
                        entity, nextAnimation, _isLoop,
                        Asserts.Code.Identifiers.UnitAnimationSpeed);

                    Attack(entity);
                }
                _attackCommandPool.Value.Del(entity);
            }
        }

        private void Attack(int unitEntity)
        {
            ref var unit = ref _unitsPool.Value.Get(unitEntity);
            ref var attackComponent = ref _attackPool.Value.Get(unitEntity);

            int targetEntity = attackComponent.Attak.Attack();
            if (-1 == targetEntity)
                return;


            if (!_healthPool.Value.Has(targetEntity))
                return;

            ref var health = ref _healthPool.Value.Get(targetEntity);
            health.Health -= unit.Avatar.DamageSize;
        }

        private const bool _isLoop = false;
    }
}
