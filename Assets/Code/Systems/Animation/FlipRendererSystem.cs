using Assets.Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.Systems.Animation
{
    sealed class FlipRendererSystem : IEcsRunSystem
    {
        private EcsPoolInject<FlipRendererComponent> 
            _flipRenderedPool = default;

        private EcsPoolInject<UnitAnimationComponent> 
            _unitAnimationPool = default;

        private EcsFilterInject<Inc<FlipRendererComponent, UnitAnimationComponent>>
            _flipFilter = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _flipFilter.Value)
            {
                ref var flip = 
                    ref _flipRenderedPool.Value.Get(entity);
                ref var unitAnimation = ref _unitAnimationPool.Value.Get(entity);

                unitAnimation.SpriteRenderer.flipX = flip.FlipX;

                _flipRenderedPool.Value.Del(entity);
            }
        }
    }
}
