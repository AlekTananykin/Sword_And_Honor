﻿using Asserts.Code;
using Assets.Code.Components;
using Assets.Code.Components.Commands;
using Assets.Code.Services;
using Assets.Code.Systems.Animation;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems
{
    public sealed class UnitWalkSystem : IEcsRunSystem
    {
        private EcsCustomInject<RendererFlipService> _renderFlipService = default;
        private EcsCustomInject<ControlAnimationService> _animationService = default;

        private EcsFilterInject<Inc<UnitComponent, MoveCommand>, 
            Exc<JumpCommand>> _moveUnitFilter = default;


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _moveUnitFilter.Value)
            {
                ref var unit = ref _moveUnitFilter.Pools.Inc1.Get(entity);

                if (unit.Avatar.IsGrounded)
                {
                    Move(ref unit, entity);
                }
                _moveUnitFilter.Pools.Inc2.Del(entity);
            }
        }

        void Move(ref UnitComponent unit, int entity)
        {
            ref var command = ref _moveUnitFilter.Pools.Inc2.Get(entity);

            unit.Transform.gameObject.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(command.Effort * unit.Avatar.StepSpeed, 0));

            _renderFlipService.Value.Flip(entity, command.Effort < 0.0f);

            _animationService.Value.StartAnimation(
                entity, Configs.AnimationTrack.walk, false,
                Identifiers.UnitAnimationSpeed);

        }
    }
}
