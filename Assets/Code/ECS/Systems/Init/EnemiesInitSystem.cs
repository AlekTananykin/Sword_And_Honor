
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.ECS.Systems.Init
{
    sealed class EnemiesInitSystem : IEcsInitSystem
    {
        public EnemiesInitSystem(string pathToConfig)
        {
            _pathToConfig = pathToConfig;
        }

        public void Init(IEcsSystems systems)
        {
            var enemiesConfig =
               Resources.Load<PlatformsLocationConfig>(_pathToConfig);

            var unitInitializer = _unitInitService.Value;

            for (int i = 0; i < enemiesConfig.Count; ++i)
            {
                var unitData = enemiesConfig[i];
                unitInitializer.Initialize(unitData.Path, unitData.Position);
            }
        }

        private EcsCustomInject<IUnitInitService> _unitInitService = default;
        private string _pathToConfig = default;
    }
}
