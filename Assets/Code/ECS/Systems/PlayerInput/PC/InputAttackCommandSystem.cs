﻿using Asserts.Code;
using Assets.Code.ECS.Components;
using Assets.Code.ECS.Components.Commands;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.PlayerInput.PC
{
    public sealed class InputAttackCommandSystem : IEcsRunSystem
    {
        readonly private EcsFilterInject<Inc<
            UnitComponent
            , IsControlledByPlayerComponent
            >, Exc<MeleeCommand>> _units = default;

        readonly private EcsPoolInject<MeleeCommand>
            _attackCommandPool = default;

        bool _isButtonDown = false;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _units.Value)
            {
                float attackEffort = Input.GetAxisRaw(Identifiers.Attack);
                if (0 != attackEffort)
                {
                    if (!_isButtonDown)
                        _attackCommandPool.Value.Add(entity);
                    
                    _isButtonDown = true;
                }
                else
                    _isButtonDown = false;
            }
        }
    }
}
