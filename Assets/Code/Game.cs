using Asserts.Code;
using Assets.Code.ECS.Animation;
using Assets.Code.ECS.Attack.HealthLoss;
using Assets.Code.ECS.Death;
using Assets.Code.ECS.Health;
using Assets.Code.ECS.Systems.Init;
using Assets.Code.ECS.Teleport;
using Assets.Code.ECS.Timer;
using Assets.Code.Fabrics;
using Assets.Code.Interfaces;
using Assets.Code.Pools;
using Assets.Code.Services;
using Assets.Code.Systems;
using Assets.Code.Systems.Animation;
using Assets.Code.Systems.Player;
using Assets.Code.Systems.PlayerInput.PC;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code
{
    public sealed class Game : MonoBehaviour
    {
        EcsSystems _systems;

        private void Start()
        {
            var world = new EcsWorld();
            _systems = new EcsSystems(world);

            var objectsPool =
               new VariousObjectsPool(new GameObjectsFabric());

            AddSystems(_systems);
            InjectServices(_systems, objectsPool);

            _systems.Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems?.GetWorld()?.Destroy();
            _systems = null;
        }

        void AddSystems(EcsSystems systems)
        {
            AddInitSystems(systems);
            AddRunSystems(systems);
        }

        private void AddRunSystems(EcsSystems systems)
        {
            new PcInputSystemsAdder(systems);

            systems
                .Add(new TimeSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Add(new UnitWalkSystem())
                .Add(new UnitJumpSystem())
                .Add(new MeleeSystem())
                .Add(new StopWalkSystem())
                .Add(new FallAfterWalkSystem())

                .Add(new TeleportSystem())
                .Add(new ScaleSystem())
                .Add(new TimerSystem())

                .Add(new FlipRendererSystem())
                .Add(new UpdateAnimationSystem())
                .Add(new JumpToFallAnimationSwitchSystem())
                .Add(new SwitchSleepAnimationToIdleSystem())
                .Add(new PlaySoundSystem())
                .Add(new HealthSystem())
                .Add(new DeathSystem())
                .Add(new CleanerSystem())

                .Add(new HealthLossInitViewSystem())
                .Add(new HealthLossDestroyViewSystem())
                ;
        }

        private void AddInitSystems(EcsSystems systems)
        {
            systems
                .Add(new PlayerInitSystem())
                .Add(new PlatformsInitSystem(
                    Identifiers.PlatformsConfigPath))
                .Add(new EnemiesInitSystem(
                    Identifiers.EnemiesLocationConfigPath))
                ;
        }

        private void InjectServices(EcsSystems systems, 
            IVariousObjectsPool objectsPool)
        {
            var animationService  = new ControlAnimationService(systems);

            var unitInitService = 
                new UnitInitService(systems, animationService, objectsPool);

            systems.Inject(
                  new TimeService()
                , new ControlSoundService(systems)
                , animationService
                , new RendererFlipService(systems)
                , objectsPool
                , unitInitService
                );
        }
    }
}
