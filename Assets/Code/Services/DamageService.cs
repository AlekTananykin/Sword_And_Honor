using Assets.Code.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Code.Services
{
    public sealed class DamageService
    {
        public DamageService(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _healthPool = world.GetPool<HealthComponent>();
        }

        public void ChangeUnitHealth(int unitEntity, float deltaHealth)
        {
            if (!_healthPool.Has(unitEntity))
            {
                Debug.Log($"DamageService::ChangeUnitHealth>>" 
                    + " there is not entity {unitEntity}");
                return;
            }

            ref var unitHealth = ref _healthPool.Get(unitEntity);
            float resultHealth = unitHealth.Health + deltaHealth;

            unitHealth.Health = (resultHealth < 0.0f)? 0.0f: resultHealth;
        }


        private EcsPool<HealthComponent> _healthPool;
    }
}
