using Assets.Code.Interfaces;
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
        [SerializeField] SceneData _sceneData;

        EcsSystems _systems;

        private void Start()
        {
            var world = new EcsWorld();
            _systems = new EcsSystems(world);

            AddSystems(_systems);
            InjectServices(_systems);
            
            _systems.Inject(_sceneData);

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
            new PcInputSystemsAdder(systems);

            systems
            .Add(new TimeSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Add(new PlayerInitSystem())
                .Add(new UnitWalkSystem())
                .Add(new UnitJumpSystem())
                .Add(new UnitAttackSystem())

                .Add(new FlipRendererSystem())
                .Add(new UpdateAnimationSystem())
                .Add(new JumpToFallAnimationSwitchSystem())
                .Add(new PlaySoundSystem()

                );
        }

        private void InjectServices(EcsSystems systems)
        {
            IControlSoundService soundService =
                new ControlSoundService(systems);

            systems.Inject(
                  new TimeService()
                , new ControlSoundService(systems)
                , new ControlAnimationService(systems, soundService)
                , new DamageService(systems)
                , new RendererFlipService(systems)
                , new CommandAnimationController(systems)
                );
        }
    }
}
