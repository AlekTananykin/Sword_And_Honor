using Assets.Code.Components;
using Assets.Code.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.Animation
{
    internal class UpdateAnimationSystem: IEcsRunSystem, IEcsInitSystem 
    {
        private EcsPoolInject<UnitAnimationComponent> 
            _unitAnimationPool = default;

        private EcsFilterInject<Inc<UnitAnimationComponent>> 
            _unitAnimationFilter = default;

        private readonly EcsCustomInject<TimeService> _timeService = default;
        ControlAnimationService _animationService = default;

        public void Init(IEcsSystems systems)
        {
            _animationService = new ControlAnimationService(systems);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var animationEntity in _unitAnimationFilter.Value)
            {
                ref var animationUnit = 
                    ref _unitAnimationPool.Value.Get(animationEntity);

                if (animationUnit.Sleeps)
                {
                    _animationService.StartAnimation(
                        animationEntity, Configs.Track.idle, true, 5.0f);
                }

                UpdateAnimation(
                    _timeService.Value.DeltaTime, ref animationUnit);

                animationUnit.SpriteRenderer.sprite = 
                    animationUnit.Sprites[(int)animationUnit.Counter];
            }
        }
        
        private void UpdateAnimation(
            float deltaTime, ref UnitAnimationComponent unitAnimation)
        {
            if (unitAnimation.Sleeps)
            {
                return;
            }

            unitAnimation.Counter += deltaTime * unitAnimation.Speed;

            if (unitAnimation.Loop)
            {
                while (unitAnimation.Counter > unitAnimation.Sprites.Count)
                    unitAnimation.Counter -= unitAnimation.Sprites.Count;
            }
            else if (unitAnimation.Counter > unitAnimation.Sprites.Count)
            {
                unitAnimation.Counter = unitAnimation.Sprites.Count - 1;
                unitAnimation.Sleeps = true;
            }
        }

    }
}
