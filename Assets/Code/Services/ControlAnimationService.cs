using Assets.Code.ECS.Components;
using Assets.Code.Configs;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using static Assets.Code.Configs.SpriteAnimationConfig;

namespace Assets.Code.Systems.Animation
{
    public sealed class ControlAnimationService: IControlAnimationService
    {
        public ControlAnimationService(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _animationContextPool = world.GetPool<AnimationContextComponent>();
            _unitAnimationPool = world.GetPool<UnitAnimationComponent>();
        }

        public void StartAnimation(int unitEntity,
            AnimationTrack track, bool isLoop, float speed)
        {
            if (_animationContextPool.Has(unitEntity))
            {
                ref var animationContext = ref _animationContextPool.Get(unitEntity);

                animationContext.Loop = isLoop;
                animationContext.Speed = speed;
                
                if (track != animationContext.Trak)
                {
                    animationContext.Sleeps = false;

                    ref var animationComponent = ref _unitAnimationPool.Get(unitEntity);

                    animationContext.Trak = track;

                    var animationClip = GetAnimationClip(ref animationComponent, track);

                    animationContext.Clip = animationClip.Clip;
                    animationContext.Counter = 0;
                }
            }
            else
            {
                ref var animationContext = 
                    ref _animationContextPool.Add(unitEntity);

                animationContext.Loop = isLoop;
                animationContext.Speed = speed;
                animationContext.Sleeps = false;

                ref var unitAnimationComponent = 
                    ref _unitAnimationPool.Get(unitEntity);

                animationContext.Trak = track;

                var animationClip = GetAnimationClip(
                    ref unitAnimationComponent, track);

                animationContext.Clip = animationClip.Clip;

                animationContext.SpriteRenderer = 
                    unitAnimationComponent.SpriteRenderer;
                
                animationContext.Counter = 0;
            }
        }

        private AnimationClip GetAnimationClip(
            ref UnitAnimationComponent animationComponent, 
            AnimationTrack track)
        {
            return animationComponent.AnimationConfig.Sequences.Find(
                    sequence => sequence.Track == track);
        }

        private EcsPool<AnimationContextComponent>
            _animationContextPool = default;

        private EcsPool<UnitAnimationComponent> _unitAnimationPool = default;
    }
}
