using Assets.Code.Components;
using Assets.Code.Configs;
using Leopotam.EcsLite;

namespace Assets.Code.Systems.Animation
{
    public sealed class ControlAnimationService
    {
        private EcsPool<AnimationTaskComponent>
            _animationTaskPool = default;

        private EcsPool<UnitAnimationComponent> _unitAnimationPool = default;

        public ControlAnimationService(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _animationTaskPool = world.GetPool<AnimationTaskComponent>();
            _unitAnimationPool = world.GetPool<UnitAnimationComponent>();
        }

        internal void StartAnimation(int unitEntity,
            Track track, bool loop, float speed)
        {
            
            if (_animationTaskPool.Has(unitEntity))
            {
                ref var animation = ref _animationTaskPool.Get(unitEntity);

                animation.Loop = loop;
                animation.Speed = speed;
                
                if (track != animation.Trak)
                {
                    animation.Sleeps = false;

                    ref var unit = ref _unitAnimationPool.Get(unitEntity);

                    animation.Trak = track;

                    animation.Sprites = unit.AnimationConfig.Sequences.Find(
                            sequence => track == sequence.Track).Sprites;

                    animation.Counter = 0;
                }
            }
            else
            {
                ref var animation = ref _animationTaskPool.Add(unitEntity);
                ref var unit = ref _unitAnimationPool.Get(unitEntity);

                animation.Trak = track;
                animation.Sprites = unit.AnimationConfig.Sequences.Find(
                    sequence => sequence.Track == track).Sprites;

                animation.SpriteRenderer = unit.SpriteRenderer;

                animation.Loop = loop;
                animation.Speed = speed;
                animation.Counter = 0;
                animation.Sleeps = false;
            }
        }

        public void StopAnimation(int entity)
        {
            if (_animationTaskPool.Has(entity))
                _animationTaskPool.Del(entity);
        }
    }
}
