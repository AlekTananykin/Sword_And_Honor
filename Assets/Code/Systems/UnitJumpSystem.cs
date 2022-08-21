using Asserts.Code;
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

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _jumpUnitFilter.Value)
            {
                ref var unit = ref _jumpUnitFilter.Pools.Inc1.Get(entity);
                
                if (unit.Avatar.IsGrounded)
                {
                    MakeJump(ref unit, entity);
                }
                _jumpUnitFilter.Pools.Inc2.Del(entity);
            }
        }

        private void MakeJump(ref UnitComponent unit, int entity)
        {
            ref var command = ref _jumpUnitFilter.Pools.Inc2.Get(entity);

            unit.Transform.gameObject.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(0.0f, command.Effort * unit.Avatar.JumpSpeed));

            _animationService.Value.StartAnimation(
                entity, Configs.AnimationTrack.jump,
                false, Identifiers.UnitAnimationSpeed);
        }

        private EcsCustomInject<IControlAnimationService>
            _animationService = default;

        private EcsFilterInject<Inc<UnitComponent, JumpCommand>>
            _jumpUnitFilter = default;
    }
}
