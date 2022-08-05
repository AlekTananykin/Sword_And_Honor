using Assets.Code.Components;
using Assets.Code.Components.Commands;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems
{
    public sealed class UnitMoveSystem : UnitSystemBase, IEcsRunSystem
    {
        private EcsFilterInject<Inc<Unit, MoveCommand>> 
            _moveUnitFilter = default;

        private EcsPoolInject<MoveCommand> _moveCommandPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _moveUnitFilter.Value)
            {
                ref var unit = ref _moveUnitFilter.Pools.Inc1.Get(entity);

                if (unit.RigidBody.velocity.magnitude <= unit.Settings.NewStepVelocitySpeed && 
                    unit.Settings.IsGrounded)
                {
                    ref var command = ref _moveUnitFilter.Pools.Inc2.Get(entity);

                    unit.Transform.gameObject.GetComponent<Rigidbody2D>().AddForce(
                        new Vector2(command.Effort * unit.Settings.StepSpeed, 0));

                    _renderFlipService.Flip(entity, command.Effort < 0.0f);
                    _animationService.StartAnimation(entity, Configs.Track.run, true, 5.0f);

                }
                _moveCommandPool.Value.Del(entity);
            }
        }
    }
}
