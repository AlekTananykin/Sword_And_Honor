using Assets.Code.Components;
using Assets.Code.Configs;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using static Assets.Code.Configs.SpriteAnimationConfig;

namespace Assets.Code.Systems.Animation
{
    public sealed class ControlAnimationService
    {
        public ControlAnimationService(IEcsSystems systems, 
            IControlSoundService soundService)
        {
            var world = systems.GetWorld();
            _animationTaskPool = world.GetPool<AnimationTaskComponent>();
            _unitAnimationPool = world.GetPool<UnitAnimationComponent>();

            _soundService = soundService;
        }

        internal void StartAnimation(int unitEntity,
            AnimationTrack track, bool isLoop, float speed)
        {
            if (_animationTaskPool.Has(unitEntity))
            {
                ref var animation = ref _animationTaskPool.Get(unitEntity);

                animation.Loop = isLoop;
                animation.Speed = speed;
                
                if (track != animation.Trak)
                {
                    animation.Sleeps = false;
                    _soundService.StopSound(unitEntity);
                    
                    ref var unit = ref _unitAnimationPool.Get(unitEntity);

                    animation.Trak = track;

                    var animationContext = GetAnimationContext(ref unit, track);

                    animation.Sprites = animationContext.Sprites;
                    animation.Counter = 0;

                    _soundService.PlaySound(unitEntity, animationContext.BeginAnimationSound, 
                        animationContext.IsLoopBeginAnimationSound);
                }
            }
            else
            {
                ref var animation = ref _animationTaskPool.Add(unitEntity);
                ref var unit = ref _unitAnimationPool.Get(unitEntity);

                animation.Trak = track;

                var animationContext = GetAnimationContext(ref unit, track);

                animation.Sprites = animationContext.Sprites;

                animation.SpriteRenderer = unit.SpriteRenderer;

                animation.Loop = isLoop;
                animation.Speed = speed;
                animation.Counter = 0;
                animation.Sleeps = false;
            }
        }

        AnimationContext GetAnimationContext(ref UnitAnimationComponent animationComponent, 
            AnimationTrack track)
        {
            return animationComponent.AnimationConfig.Sequences.Find(
                    sequence => sequence.Track == track);
        }

        public void StopAnimation(int entity)
        {
            if (_animationTaskPool.Has(entity))
                _animationTaskPool.Del(entity);
        }

        private EcsPool<AnimationTaskComponent>
            _animationTaskPool = default;

        private EcsPool<UnitAnimationComponent> _unitAnimationPool = default;

        private IControlSoundService _soundService;
    }
}
