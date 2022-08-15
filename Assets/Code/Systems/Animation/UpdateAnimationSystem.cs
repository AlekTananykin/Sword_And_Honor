using Assets.Code.Components;
using Assets.Code.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.Animation
{
    internal class UpdateAnimationSystem: IEcsRunSystem 
    {
        private EcsPoolInject<AnimationTaskComponent> 
            _animationTaskPool = default;

        private EcsFilterInject<Inc<AnimationTaskComponent>> 
            _animationTaskFilterFilter = default;

        private readonly EcsCustomInject<TimeService> _timeService = default;

        private EcsCustomInject<ControlAnimationService> _animationService = default;


        public void Run(IEcsSystems systems)
        {
            foreach (var animationEntity in _animationTaskFilterFilter.Value)
            {
                ref var animationUnit = 
                    ref _animationTaskPool.Value.Get(animationEntity);

                if (animationUnit.Sleeps)
                {
                    _animationService.Value.StartAnimation(
                        animationEntity, Configs.AnimationTrack.idle, true, 5.0f);
                }

                UpdateAnimation(
                    _timeService.Value.DeltaTime, ref animationUnit);

                animationUnit.SpriteRenderer.sprite = 
                    animationUnit.Sprites[(int)animationUnit.Counter];
            }
        }
        
        private void UpdateAnimation(
            float deltaTime, ref AnimationTaskComponent unitAnimation)
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
