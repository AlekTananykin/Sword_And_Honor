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

            var timeService = new TimeService();
            var animationService = new ControlAnimationService();

            new PcInputSystemsAdder(_systems);

            _systems
                .Add(new TimeSystem())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Add(new PlayerInitSystem())
                .Add(new UnitMoveSystem())
                .Add(new UnitJumpSystem())
                .Inject(timeService, animationService, _sceneData)
                .Init();
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
    }
}
