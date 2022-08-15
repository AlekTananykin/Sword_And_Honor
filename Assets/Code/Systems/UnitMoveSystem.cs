using Assets.Code.Components;
using Assets.Code.Components.Commands;
using Assets.Code.Interfaces;
using Assets.Code.Services;
using Assets.Code.Systems.Animation;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems
{
    public sealed class UnitMoveSystem : IEcsRunSystem
    {
        private EcsCustomInject<RendererFlipService> _renderFlipService;
        private EcsCustomInject<ControlAnimationService> _animationService;

        private EcsFilterInject<Inc<UnitComponent, MoveCommand>> 
            _moveUnitFilter = default;

        private EcsPoolInject<MoveCommand> _moveCommandPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _moveUnitFilter.Value)
            {
                ref var unit = ref _moveUnitFilter.Pools.Inc1.Get(entity);

                if (unit.RigidBody.velocity.magnitude <= unit.Avatar.NewStepVelocitySpeed && 
                    unit.Avatar.IsGrounded)
                {
                    ref var command = ref _moveUnitFilter.Pools.Inc2.Get(entity);

                    unit.Transform.gameObject.GetComponent<Rigidbody2D>().AddForce(
                        new Vector2(command.Effort * unit.Avatar.StepSpeed, 0));

                    _renderFlipService.Value.Flip(entity, command.Effort < 0.0f);
                    _animationService.Value.StartAnimation(
                        entity, Configs.AnimationTrack.run, true, 5.0f);

                }
                _moveCommandPool.Value.Del(entity);
            }
        }
    }
}
