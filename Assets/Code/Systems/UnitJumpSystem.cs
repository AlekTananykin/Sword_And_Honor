using Assets.Code.Components;
using Assets.Code.Components.Commands;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems
{
    public sealed class UnitJumpSystem : IEcsRunSystem
    {
        private EcsCustomInject<IControlAnimationService> _animationService;

        private EcsFilterInject<Inc<
            UnitComponent, JumpCommand, IsReadyToGetCommandComponent>>
            _jumpUnitFilter = default;

        private EcsPoolInject<JumpCommand> _jumpCommandPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _jumpUnitFilter.Value)
            {
                ref var unit = ref _jumpUnitFilter.Pools.Inc1.Get(entity);

                if (unit.Avatar.IsGrounded)
                {
                    ref var command = ref _jumpUnitFilter.Pools.Inc2.Get(entity);
                    unit.Transform.gameObject.GetComponent<Rigidbody2D>().AddForce(
                        new Vector2(0, command.Effort * unit.Avatar.StepSpeed));
                }
                _jumpCommandPool.Value.Del(entity);

                _animationService.Value.StartAnimation(
                    entity, Configs.AnimationTrack.jump, false, 5.0f);
            }
        }
    }
}
