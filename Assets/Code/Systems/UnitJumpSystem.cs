﻿using Assets.Code.Components.Commands;
using Assets.Code.Systems.Animation;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems
{
    public sealed class UnitJumpSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Unit, JumpCommand>>
            _jumpUnitFilter = default;

        private EcsPoolInject<JumpCommand> _jumpCommandPool = default;

        private EcsCustomInject<ControlAnimationService> 
            _animationService = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _jumpUnitFilter.Value)
            {
                ref var unit = ref _jumpUnitFilter.Pools.Inc1.Get(entity);

                if (unit.Settings.IsGrounded)
                {
                    ref var command = ref _jumpUnitFilter.Pools.Inc2.Get(entity);
                    unit.Transform.gameObject.GetComponent<Rigidbody2D>().AddForce(
                        new Vector2(0, command.Effort * unit.Settings.StepSpeed));
                }
                _jumpCommandPool.Value.Del(entity);

                _animationService.Value.StartAnimation(
                    entity, Configs.Track.jump, false, 5.0f);
            }
        }
    }
}
