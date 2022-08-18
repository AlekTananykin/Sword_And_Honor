using Asserts.Code;
using Assets.Code.Components;
using Assets.Code.Components.Commands;
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
            , IsReadyToGetCommandComponent
            >> _units = default;

        readonly private EcsPoolInject<AttackCommand>
            _attackCommandPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _units.Value)
            {
                float attackEffort = Input.GetAxisRaw(Identifiers.Attack);
                if (0 != attackEffort)
                    _attackCommandPool.Value.Add(entity);
            }
        }
    }
}
