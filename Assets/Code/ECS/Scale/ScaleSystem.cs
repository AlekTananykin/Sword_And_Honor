

using Assets.Code.ECS.Components;
using Assets.Code.ECS.Scale;
using Assets.Code.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class ScaleSystem : IEcsRunSystem
{
    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter.Value)
        {
            ref var transform = ref _filter.Pools.Inc1.Get(entity);
            ref var scale = ref _filter.Pools.Inc2.Get(entity);

            float xScaleChange = 
                scale.ScaleSpeed * _timeService.Value.DeltaTime;

            float yScaleChange = 
                scale.ScaleSpeed * _timeService.Value.DeltaTime;

            transform.Transform.localScale += new Vector3(xScaleChange, yScaleChange, 0.0f);
        }
    }

    EcsCustomInject<TimeService> _timeService;
    EcsFilterInject<Inc<TransformComponent, ScaleComponent>> _filter = default;

}
