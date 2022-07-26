using Leopotam.EcsLite;

namespace Assets.Code.Systems.PlayerInput.PC
{
    public class PcInputSystemsAdder
    {
        public PcInputSystemsAdder(EcsSystems systems)
        {
            systems
                .Add(new InputAttackCommandSystem())
                .Add(new InputDefendCommandSystem())
                .Add(new InputJumpCommandSystem())
                .Add(new InputMoveCommandSystem())
                ;
        }
    }
}
