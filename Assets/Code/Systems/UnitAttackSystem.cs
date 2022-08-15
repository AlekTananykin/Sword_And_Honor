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

        private EcsFilterInject<Inc<UnitComponent, AttackCommand>>
            _attackUnitFilter = default;

        private EcsPoolInject<AttackCommand> _attackCommandPool = default;
        private EcsPoolInject<AttackComponent> _attackPool = default;

        private EcsPoolInject<UnitComponent> _unitsPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _attackUnitFilter.Value)
            {
                _animationService.Value.StartAnimation(
                    entity, Configs.AnimationTrack.attack1, _isLoop, 
                    Asserts.Code.Identifiers.UnitAnimationSpeed);

                Attack(entity);

                _attackCommandPool.Value.Del(entity);
            }
        }

        private void Attack(int unitEntity)
        {
            ref var unit = ref _unitsPool.Value.Get(unitEntity);
            ref var attackComponent = ref _attackPool.Value.Get(unitEntity);

            attackComponent.Attak.Attack();
        }

        private const bool _isLoop = false;
    }
}
