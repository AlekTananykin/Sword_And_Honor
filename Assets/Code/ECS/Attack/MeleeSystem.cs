using Asserts.Code;
using Assets.Code.ECS.Animation;
using Assets.Code.ECS.Components;
using Assets.Code.ECS.Components.Commands;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

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

        private EcsWorldInject _world;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _attackUnitFilter.Value)
            {
                Configs.AnimationTrack nextAnimation =
                    Configs.AnimationTrack.attack1;

                _animationService.Value.StartAnimation(
                        entity, nextAnimation, false,
                        Asserts.Code.Identifiers.UnitAnimationSpeed);

                Attack(entity);

                _attackCommandPool.Value.Del(entity);
            }
        }

        private void Attack(int unitEntity)
        {
            ref var attackComponent = ref _attackPool.Value.Get(unitEntity);

            int targetEntity = attackComponent.Attak.Attack();
            if (-1 == targetEntity)
                return;

            int damageEntity = _world.Value.NewEntity();
            ref var healthChange = ref _healthChangePool.Value.Add(damageEntity);

            healthChange.DeltaHealth = -Identifiers.Damege;
            healthChange.Target = targetEntity;

            Debug.Log("Delta health: " + healthChange.DeltaHealth);

            if (!_switchToIdlePool.Value.Has(unitEntity))
                _switchToIdlePool.Value.Add(unitEntity);
        }
    }
}
