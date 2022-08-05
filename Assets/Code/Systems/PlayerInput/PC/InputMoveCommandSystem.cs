﻿using Asserts.Code;
using Assets.Code.Components;
using Assets.Code.Components.Commands;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.PlayerInput.PC
{
    public sealed class InputMoveCommandSystem : IEcsRunSystem
    {
        readonly private EcsFilterInject<Inc<Unit, ControlledByPlayer>>
            _units = default;

        readonly private EcsPoolInject<MoveCommand>
            _moveCommandPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _units.Value)
            {
                var direction = Input.GetAxisRaw(Identifiers.HorizontalAxis);

                if (0 == direction)
                    continue;

                ref var move = ref _moveCommandPool.Value.Add(entity);
                move.Effort = direction;

            }
        }
    }
}
