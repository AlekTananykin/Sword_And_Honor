using Assets.Code.Services;
using Assets.Code.Systems.Animation;
using Leopotam.EcsLite;

namespace Assets.Code.Systems
{
    public class UnitSystemBase : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            _renderFlipService = new RendererFlipService(systems);
            _animationService = new ControlAnimationService(systems);
        }

        protected RendererFlipService _renderFlipService = default;
        protected ControlAnimationService _animationService = default;
    }
}
