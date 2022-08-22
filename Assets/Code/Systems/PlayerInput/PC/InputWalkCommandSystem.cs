using Asserts.Code;
using Assets.Code.Components;
using Assets.Code.Components.Commands;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.PlayerInput.PC
{
    public sealed class InputWalkCommandSystem : IEcsRunSystem
    {
        readonly private EcsFilterInject<Inc
            <UnitComponent
            , IsControlledByPlayerComponent>, Exc<MoveCommand>>_units = default;

        readonly private EcsPoolInject<MoveCommand>
            _moveCommandPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _units.Value)
            {
                var effort = Input.GetAxisRaw(Identifiers.HorizontalAxis);

                if (0 == effort)
                    continue;

                ref var move = ref _moveCommandPool.Value.Add(entity);
                move.Effort = effort;
            }
        }
    }
}
