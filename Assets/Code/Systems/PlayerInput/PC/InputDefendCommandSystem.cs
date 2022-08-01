using Asserts.Code;
using Assets.Code.Components.Commands;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.PlayerInput.PC
{
    public sealed class InputDefendCommandSystem : IEcsRunSystem
    {
        readonly private EcsFilterInject<Inc<Unit, ControlledByPlayer>>
            _units = default;

        readonly private EcsPoolInject<DefendCommand>
            _defendCommandPool = default;

        public void Run(IEcsSystems systems)
        {
            return;
            foreach (var entity in _units.Value)
            {
                float defendEffort = Input.GetAxisRaw(Identifiers.Defend);
                if (0 != defendEffort)
                {
                    _defendCommandPool.Value.Add(entity);
                }
            }
        }
    }
}
