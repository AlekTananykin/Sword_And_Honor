using Asserts.Code;
using Assets.Code.Components.Commands;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.PlayerInput.PC
{
    public sealed class InputJumpCommandSystem : IEcsRunSystem
    {
        private EcsPoolInject<JumpCommand> _jumpCommandPool = default;
        private EcsFilterInject<Inc<Unit, ControlledByPlayer>> 
            _playerUnits = default;



        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _playerUnits.Value)
            {
                float effort = Input.GetAxisRaw(Identifiers.Jump);
                if (0.0f == effort)
                    continue;
                
                ref var jump = ref _jumpCommandPool.Value.Add(entity);
                jump.Effort = effort;
                
            }
        }
    }
}
