using Assets.Code.ECS.Animation;
using Assets.Code.ECS.Components;
using Assets.Code.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.Systems.Animation
{
    internal class UpdateAnimationSystem: IEcsRunSystem 
    {
        public void Run(IEcsSystems systems)
        {
            foreach (var animationEntity in _animationTaskFilter.Value)
            {
                ref var animationUnit = 
                    ref _animationContextPool.Value.Get(animationEntity);

                UpdateAnimation(animationEntity,
                    _timeService.Value.DeltaTime, ref animationUnit);

                var animationClip = 
                    animationUnit.Clip[(int)animationUnit.Counter];

                animationUnit.SpriteRenderer.sprite = animationClip.Sprite;

                _soundService.Value.PlaySound(
                    animationEntity, animationClip.AudioClip, false);
            }
        }
        
        private void UpdateAnimation(int animationEntity,
            float deltaTime, ref AnimationContextComponent unitAnimation)
        {
            unitAnimation.Counter += deltaTime * unitAnimation.Speed;

            if (unitAnimation.Loop)
            {
                while (unitAnimation.Counter > unitAnimation.Clip.Count)
                    unitAnimation.Counter -= unitAnimation.Clip.Count;
            }
            else if (unitAnimation.Counter > unitAnimation.Clip.Count)
            {
                unitAnimation.Counter = unitAnimation.Clip.Count - 1;
                _isSleepPool.Value.Add(animationEntity);
            }
        }

        private EcsPoolInject<AnimationContextComponent>
           _animationContextPool = default;

        private EcsFilterInject<Inc<AnimationContextComponent>, 
            Exc<IsSleep>> _animationTaskFilter = default;

        private EcsPoolInject<IsSleep> _isSleepPool = default;

        private readonly EcsCustomInject<TimeService> _timeService = default;

        private EcsCustomInject<ControlSoundService> _soundService = default;
    }
}
