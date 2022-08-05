
using Assets.Code.Components;
using Leopotam.EcsLite;

namespace Assets.Code.Services
{
    public sealed class RendererFlipService
    {
        public RendererFlipService(IEcsSystems systems)
        {
            _FlipRendererComponent = 
                systems.GetWorld().GetPool<FlipRendererComponent>();
        }


        public void Flip(int entity, bool flipX)
        {
            ref var flipComponent = ref _FlipRendererComponent.Add(entity);
            flipComponent.FlipX = flipX;
        }

        private EcsPool<FlipRendererComponent> _FlipRendererComponent;

    }
}
