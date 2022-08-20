using Asserts.Code;
using Assets.Code.Components;
using Assets.Code.Components.Commands;
using Assets.Code.Components.Unit;
using Assets.Code.Services;
using Assets.Code.Systems.Animation;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems
{
    public sealed class UnitWalkSystem : IEcsRunSystem
    {
        private EcsPoolInject<IsReadyToGetCommandComponent>
            _isReadyToGetCommandPool = default;
        private EcsCustomInject<RendererFlipService> _renderFlipService = default;
        private EcsCustomInject<ControlAnimationService> _animationService = default;

        private EcsFilterInject<Inc<UnitComponent, MoveCommand, StepComponent>, 
            Exc<JumpCommand>> 
            _moveUnitFilter = default;

        private EcsPoolInject<MoveCommand> _moveCommandPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _moveUnitFilter.Value)
            {
                ref var unit = ref _moveUnitFilter.Pools.Inc1.Get(entity);

                if (unit.Avatar.IsGrounded && 
                    _isReadyToGetCommandPool.Value.Has(entity))
                {
                    MakeStep(ref unit, entity);
                }
                _moveCommandPool.Value.Del(entity);
            }
        }

        void MakeStep(ref UnitComponent unit, int entity)
        {
            ref var command = ref _moveUnitFilter.Pools.Inc2.Get(entity);

            unit.Transform.gameObject.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(command.Effort * unit.Avatar.StepSpeed, 0));

            _renderFlipService.Value.Flip(entity, command.Effort < 0.0f);

            ref var step = ref _moveUnitFilter.Pools.Inc3.Get(entity);

            if (step.IsLeftLeg)
            {
                _animationService.Value.StartAnimation(
                    entity, Configs.AnimationTrack.leftLegstep, false,
                    Identifiers.UnitAnimationSpeed);
            }
            else
            {
                _animationService.Value.StartAnimation(
                    entity, Configs.AnimationTrack.rightLegStep, false,
                    Identifiers.UnitAnimationSpeed);
            }
            step.IsLeftLeg = !step.IsLeftLeg;
        }
    }
}
