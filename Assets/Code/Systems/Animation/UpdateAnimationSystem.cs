using Assets.Code.Components;
using Assets.Code.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.Animation
{
    internal class UpdateAnimationSystem: IEcsRunSystem, IEcsInitSystem 
    {
        private EcsPoolInject<AnimationTaskComponent> 
            _animationTaskPool = default;

        private EcsFilterInject<Inc<AnimationTaskComponent>> 
            _animationTaskFilterFilter = default;

        private readonly EcsCustomInject<TimeService> _timeService = default;
        ControlAnimationService _animationService = default;

        public void Init(IEcsSystems systems)
        {
            var soundService = new ControlSoundService(systems);
            _animationService = new ControlAnimationService(systems, soundService);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var animationEntity in _animationTaskFilterFilter.Value)
            {
                ref var animationUnit = 
                    ref _animationTaskPool.Value.Get(animationEntity);

                if (animationUnit.Sleeps)
                {
                    _animationService.StartAnimation(
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
