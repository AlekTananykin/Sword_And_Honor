using Assets.Code.ECS.Components;
using Assets.Code.ECS.Timer;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.ECS.Attack.HealthLoss
{
    sealed class HealthLossDestroyViewSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _healthLossFilter.Value)
            {
                var gameObject = _healthLossFilter.
                    Pools.Inc3.Get(entity).Instance;

                gameObject.active = false;
                _objectsPool.Value.Intake(ref gameObject);
            }
        }

        EcsFilterInject<Inc<HealthLoss, 
            TimerTaskIsCompletedComponent, 
            GameObjectComponent>> _healthLossFilter = default;

        EcsCustomInject<IVariousObjectsPool> _objectsPool = default;
    }
}
