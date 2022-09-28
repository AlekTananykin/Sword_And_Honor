using Assets.Code.ECS.Animation;
using Assets.Code.ECS.Components;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.ECS.Death
{
    sealed class CleanerSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            foreach (var entity in _readyFilter.Value) 
            {
                ref var gameObject = ref _readyFilter.Pools.Inc3.Get(entity);

                gameObject.Instance.SetActive(false);

                _objectsPool.Value.Intake(ref gameObject.Instance);

                world.DelEntity(entity);
            }
        }

        private EcsFilterInject<Inc<IsDead, IsSleep, 
            GameObjectComponent>> _readyFilter = default;

        private EcsCustomInject<IUnitInitService>
            _unitInitService = default;

        private EcsCustomInject<IVariousObjectsPool> _objectsPool = default;
    }
}
