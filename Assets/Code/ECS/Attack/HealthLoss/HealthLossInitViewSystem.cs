using Assets.Code.ECS.Components;
using Assets.Code.ECS.Scale;
using Assets.Code.ECS.Teleport;
using Assets.Code.ECS.Timer;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using UnityEngine;

namespace Assets.Code.ECS.Attack.HealthLoss
{
    public class HealthLossInitViewSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            foreach (var digitEntity in _healthLossFilter.Value)
            {
                ref var healthLoss = ref _healthLossFilter.Pools.Inc1.Get(digitEntity);

                var digitView = _gameObjectsCreater.Value.GetGameObject(
                    healthLoss.LossViewPrefabPath);

                _objectPool.Value.Add(digitEntity).Instance = digitView;

                SetPosition(ref healthLoss, digitEntity, digitView);

                ref var teleport = ref _teleportPool.Value.Add(digitEntity);
                teleport.Velocity = new UnityEngine.Vector3(0.0f, 0.2f, 0.0f);

                ref var scale = ref _scalePool.Value.Add(digitEntity);
                scale.ScaleSpeed = 0.1f;
                digitView.transform.localScale = new UnityEngine.Vector3(0.1f, 0.1f, 1f);

                ref var timer = ref _timerPool.Value.Add(digitEntity);
                timer.Timer = 5.0f;

                digitView.SetActive(true);
            }
        }

        private void SetPosition(ref HealthLoss healthLoss, 
            int digitEntity, GameObject digitView)
        {
            ref var digitTransform = ref _transformPool.Value.Add(digitEntity);
            digitTransform.Transform = digitView.transform;
            digitTransform.Transform.position = healthLoss.InitPosition;
        }

        private EcsFilterInject<Inc<HealthLoss>, 
            Exc<TeleportMoveComonent, ScaleComponent, TimerComponent, TransformComponent> > 
            _healthLossFilter = default;

        private EcsPoolInject<TeleportMoveComonent> _teleportPool = default;
        private EcsPoolInject<ScaleComponent> _scalePool = default;
        private EcsPoolInject<TransformComponent> _transformPool = default;
        private EcsPoolInject<GameObjectComponent> _objectPool = default;
        private EcsPoolInject<TimerComponent> _timerPool = default;

        private EcsCustomInject<IVariousObjectsPool> _gameObjectsCreater = default;
    }
}