using Assets.Code.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.ECS.Timer
{
    sealed class TimerSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            foreach (var timeEntity in _timerPool.Value)
            {
                ref var timer = ref _timerPool.Pools.Inc1.Get(timeEntity);
                timer.Timer -= _timeService.Value.DeltaTime;

                if (timer.Timer > 0)
                    continue;

                _timerPool.Pools.Inc1.Del(timeEntity);
                _completedPool.Value.Add(timeEntity);
            }
        }

        private EcsCustomInject<TimeService> _timeService = default;
        private EcsFilterInject<Inc<TimerComponent>> _timerPool = default;
        private EcsPoolInject<TimerTaskIsCompletedComponent> _completedPool = default;
    }
}
