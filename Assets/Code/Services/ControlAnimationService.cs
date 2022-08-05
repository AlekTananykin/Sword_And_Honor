using Assets.Code.Components;
using Assets.Code.Configs;
using Leopotam.EcsLite;

namespace Assets.Code.Systems.Animation
{
    public sealed class ControlAnimationService
    {
        private EcsPool<UnitAnimationComponent>
            _unitAnimationPool = default;

        private EcsPool<Unit> _units = default;

        public ControlAnimationService(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _unitAnimationPool = world.GetPool<UnitAnimationComponent>();
            _units = world.GetPool<Unit>();
        }

        internal void StartAnimation(int unitEntity,
            Track track, bool loop, float speed)
        {
            
            if (_unitAnimationPool.Has(unitEntity))
            {
                ref var animation = ref _unitAnimationPool.Get(unitEntity);

                animation.Loop = loop;
                animation.Speed = speed;
                
                if (track != animation.Trak)
                {
                    animation.Sleeps = false;

                    ref var unit = ref _units.Get(unitEntity);

                    animation.Trak = track;

                    animation.Sprites = unit.AnimationConfig.Sequences.Find(
                            sequence => track == sequence.Track).Sprites;

                    animation.Counter = 0;
                }
            }
            else
            {
                ref var animation = ref _unitAnimationPool.Add(unitEntity);
                ref var unit = ref _units.Get(unitEntity);

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
            if (_unitAnimationPool.Has(entity))
                _unitAnimationPool.Del(entity);
        }
    }
}
