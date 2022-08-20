using Asserts.Code;
using Assets.Code.Components;
using Assets.Code.Components.Commands;
using Assets.Code.Interfaces;
using Assets.Code.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems
{
    public sealed class UnitJumpSystem : IEcsRunSystem
    {
        private EcsCustomInject<RendererFlipService> _renderFlipService = default;
        private EcsCustomInject<IControlAnimationService> _animationService;

        private EcsFilterInject<Inc<
            UnitComponent, JumpCommand>>
            _jumpUnitFilter = default;

        private EcsPoolInject<IsReadyToGetCommandComponent> 
            _isReadyToGetCommandPool = default;

        private EcsPoolInject<MoveCommand> _moveCommandPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _jumpUnitFilter.Value)
            {
                ref var unit = ref _jumpUnitFilter.Pools.Inc1.Get(entity);
                
                if (unit.Avatar.IsGrounded &&
                    _isReadyToGetCommandPool.Value.Has(entity))
                {
                    MakeJump(ref unit, entity);
                }
                _jumpUnitFilter.Pools.Inc2.Del(entity);
            }
        }

        private void MakeJump(ref UnitComponent unit, int entity)
        {
            ref var command = ref _jumpUnitFilter.Pools.Inc2.Get(entity);

            float horizontalEffort = 0.0f;
            if (_moveCommandPool.Value.Has(entity))
            {
                ref var moveCommand = ref _moveCommandPool.Value.Get(entity);
                horizontalEffort = moveCommand.Effort;
                _renderFlipService.Value.Flip(entity, horizontalEffort < 0.0f);

                _moveCommandPool.Value.Del(entity);
            }

            unit.Transform.gameObject.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(horizontalEffort * unit.Avatar.StepSpeed,
                command.Effort * unit.Avatar.JumpSpeed));

            _animationService.Value.StartAnimation(
                entity, Configs.AnimationTrack.jump,
                false, Identifiers.UnitAnimationSpeed);
        }
    }
}
