using Asserts.Code;
using Assets.Code.Components;
using Assets.Code.Components.Commands;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.PlayerInput.PC
{
    public sealed class InputStopMoveCommandSystem : IEcsRunSystem
    {
        readonly private EcsFilterInject<
            Inc<UnitComponent, IsControlledByPlayerComponent> >
            _unitEntities = default;

        readonly private EcsPoolInject<StopMoveCommand>
            _idleCommandPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _unitEntities.Value)
            {
                if (Input.GetButtonUp(Identifiers.HorizontalAxis))
                {
                    _idleCommandPool.Value.Add(entity);
                }
            }
        }
    }
}
