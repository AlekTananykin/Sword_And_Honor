using Assets.Code.Components.Commands;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;


namespace Assets.Code.Services
{
    public sealed class CommandAnimationController: ICommandAnimationController
    {
        public CommandAnimationController(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _isReadyToGetCommandComponent =
                world.GetPool<IsReadyToGetCommandComponent>();
        }

        public bool IsReady(int entity)
        {
            return _isReadyToGetCommandComponent.Has(entity);
        }

        public ref IsReadyToGetCommandComponent GetReadyComponent(int entity)
        {
            return ref _isReadyToGetCommandComponent.Get(entity);
        }


        private EcsPool<IsReadyToGetCommandComponent> 
            _isReadyToGetCommandComponent = default;
    }
}
