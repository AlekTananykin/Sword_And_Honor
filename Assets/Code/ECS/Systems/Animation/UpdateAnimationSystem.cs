using Asserts.Code;
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
            foreach (var animationEntity in _animationTaskFilterFilter.Value)
            {
                ref var animationUnit = 
                    ref _animationTaskPool.Value.Get(animationEntity);

                if (animationUnit.Sleeps)
                {
                    _animationService.Value.StartAnimation(
                        animationEntity, Configs.AnimationTrack.idle, true, 
                        Identifiers.UnitAnimationSpeed);

                    continue;
                }

                UpdateAnimation(
                    _timeService.Value.DeltaTime, ref animationUnit);

                var animationClip = 
                    animationUnit.Clip[(int)animationUnit.Counter];

                animationUnit.SpriteRenderer.sprite = animationClip.Sprite;

                _soundService.Value.PlaySound(
                    animationEntity, animationClip.AudioClip, false);
            }
        }
        
        private void UpdateAnimation(
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
                unitAnimation.Sleeps = true;
            }
        }

        private EcsPoolInject<AnimationContextComponent>
           _animationTaskPool = default;

        private EcsFilterInject<Inc<AnimationContextComponent>, 
            Exc<IsActive>> _animationTaskFilterFilter = default;

        private readonly EcsCustomInject<TimeService> _timeService = default;

        private EcsCustomInject<ControlAnimationService> 
            _animationService = default;

        private EcsCustomInject<ControlSoundService> _soundService = default;
    }
}
