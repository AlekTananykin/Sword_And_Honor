﻿using Assets.Code.ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.ECS.Health
{
    sealed class HealthSystem : IEcsRunSystem
    {
        public void Run(IEcsSystems systems)
        {
            foreach (var changeEntity in _changePool.Value)
            {
                ref var change = ref _changePool.Pools.Inc1.Get(changeEntity);

                if (_healthPool.Value.Has(change.Target))
                {
                    ref var health = ref _healthPool.Value.Get(change.Target);

                    health.Health += change.DeltaHealth;

                    if (health.Health < 0)
                        health.Health = 0;

                    if (health.Health > health.MaxHealth)
                        health.Health = health.MaxHealth;
                }
                _changePool.Pools.Inc1.Del(changeEntity);
            }
        }

        EcsFilterInject<Inc<HealthChangeComponent>> _changePool = default;
        EcsPoolInject<HealthComponent> _healthPool = default;
    }
}
