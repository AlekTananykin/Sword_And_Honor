using Asserts.Code;
using Assets.Code.ECS.Animation;
using Assets.Code.ECS.Attack.HealthLoss;
using Assets.Code.ECS.Components;
using Assets.Code.ECS.Components.Commands;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.Systems
{
    public sealed class MeleeSystem : IEcsRunSystem
    {
        private EcsCustomInject<IControlAnimationService> 
            _animationService = default;

        private EcsFilterInject<Inc<UnitComponent
            , MeleeCommand> >
            _attackUnitFilter = default;

        private EcsPoolInject<MeleeCommand> _attackCommandPool = default;
        private EcsPoolInject<MelleeComponent> _attackPool = default;

        private EcsPoolInject<HealthChangeComponent> _healthChangePool = default;
        private EcsPoolInject<IsNeedSwitchToIdle> _switchToIdlePool = default;

        private EcsPoolInject<TransformComponent> _transformPool = default;

        private EcsPoolInject<HealthLoss> _healthLossPool = default;

        private EcsWorldInject _world = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _attackUnitFilter.Value)
            {
                Configs.AnimationTrack nextAnimation =
                    Configs.AnimationTrack.attack1;

                _animationService.Value.StartAnimation(
                        entity, nextAnimation, false,
                        Asserts.Code.Identifiers.UnitAnimationSpeed);

                if (!_switchToIdlePool.Value.Has(entity))
                    _switchToIdlePool.Value.Add(entity);


                Attack(entity);

                _attackCommandPool.Value.Del(entity);
            }
        }

        private void Attack(int unitEntity)
        {
            ref var attackComponent = ref _attackPool.Value.Get(unitEntity);

            int targetEntity = attackComponent.Attak.Attack();
            if (0 > targetEntity)
                return;

            DecreaseHealth(targetEntity);
            ShowHealthLoss(targetEntity);
        }

        private void ShowHealthLoss(int targetEntity)
        {
            int healthLossEntity = _world.Value.NewEntity();
            ref var healthLoss = ref _healthLossPool.Value.Add(healthLossEntity);
            healthLoss.LossViewPrefabPath = Identifiers.MinusFiveView;

            ref var targetObject = ref _transformPool.Value.Get(targetEntity);

            healthLoss.InitPosition = targetObject.Transform.position;
        }

        private void DecreaseHealth(int targetEntity)
        {
            int damageEntity = _world.Value.NewEntity();
            ref var healthChange = ref _healthChangePool.Value.Add(damageEntity);

            healthChange.DeltaHealth = -Identifiers.Damege;
            healthChange.Target = targetEntity;
        }
    }
}
