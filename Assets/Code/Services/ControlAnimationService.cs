using Assets.Code.Components;
using Assets.Code.Configs;
using Leopotam.EcsLite.Di;

namespace Assets.Code.Systems.Animation
{
    sealed class ControlAnimationService
    {
        private EcsPoolInject<UnitAnimationComponent>
            _unitAnimationPool = default;

        private EcsPoolInject<Unit> _units = default;

        internal void StartAnimation(int unitEntity,
            Track track, bool loop, float speed)
        {
            if (_unitAnimationPool.Value.Has(unitEntity))
            {
                ref var animation = ref _unitAnimationPool.Value.Get(unitEntity);

                animation.Loop = loop;
                animation.Speed = speed;

                if (track != animation.Trak)
                {
                    ref var unit = ref _units.Value.Get(unitEntity);

                    animation.Trak = track;

                    animation.Sprites = unit.Config.Sequences.Find(
                            sequence => track == sequence.Track).Sprites;

                    animation.Counter = 0;
                }
            }
            else
            {
                ref var animation = ref _unitAnimationPool.Value.Get(unitEntity);
                ref var unit = ref _units.Value.Get(unitEntity);

                animation.Trak = track;
                animation.Sprites = unit.Config.Sequences.Find(
                    sequence => sequence.Track == track).Sprites;

                animation.Loop = loop;
                animation.Speed = speed;
                animation.Counter = 0;
                animation.Sleeps = false;
            }
        }

        public  void StopAnimation(int entity)
        {
            if (_unitAnimationPool.Value.Has(entity))
                _unitAnimationPool.Value.Del(entity);
        }
    }
}
