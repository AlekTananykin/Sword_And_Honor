using Assets.Code.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems
{
    class TimeSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsCustomInject<TimeService> _timeService = default;

        public void Init(IEcsSystems systems)
        {
            Run(systems);
        }

        public void Run(IEcsSystems systems)
        {
            _timeService.Value.DeltaTime = Time.deltaTime;
            _timeService.Value.Time = Time.time;
        }
    }
}
