﻿using Assets.Code.Components.Commands;
using Assets.Code.Systems.Animation;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems
{
    public sealed class UnitMoveSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Unit, MoveCommand>> 
            _moveUnitFilter = default;

        private EcsPoolInject<MoveCommand> _moveCommandPool = default;

        EcsCustomInject<ControlAnimationService> _animationService = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _moveUnitFilter.Value)
            {
                ref var unit = ref _moveUnitFilter.Pools.Inc1.Get(entity);

                //unit is grounded

                ref var command = ref _moveUnitFilter.Pools.Inc2.Get(entity);

                unit.Transform.gameObject.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(command.Effort * 5f, 0));

                _animationService.Value.StartAnimation(entity, Configs.Track.run, false, 5.0f);

                _moveCommandPool.Value.Del(entity);
            }
        }
    }
}
