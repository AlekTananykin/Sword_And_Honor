using Assets.Code.Components.Commands;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

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


                _jumpCommandPool.Value.Add(entity);
            }
        }
    }
}
