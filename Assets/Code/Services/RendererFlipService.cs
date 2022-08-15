
using Assets.Code.Components;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;

namespace Assets.Code.Services
{
    public sealed class RendererFlipService: IRendererFlipService
    {
        public RendererFlipService(IEcsSystems systems)
        {
            _flipRendererComponent = 
                systems.GetWorld().GetPool<FlipRendererComponent>();
        }

        public void Flip(int entity, bool flipX)
        {
            ref var flipComponent = ref _flipRendererComponent.Add(entity);
            flipComponent.FlipX = flipX;
        }

        private EcsPool<FlipRendererComponent> _flipRendererComponent;
    }
}
