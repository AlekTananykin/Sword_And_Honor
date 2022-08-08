using Asserts.Code;
using Assets.Code.Components;
using Assets.Code.Fabrics;
using Assets.Code.Systems.Animation;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.Player
{
    public sealed class PlayerInitSystem : IEcsInitSystem
    {
        EcsPoolInject<Unit> _unitPool = default;
        EcsPoolInject<UnitAnimationComponent> _unitAnimationPool = default;
        EcsPoolInject<UnitSoundComponent> _unitSoundComponent = default;

        EcsPoolInject<ControlledByPlayer> _isControllerByPlayer = default;
                
        ControlAnimationService _animationService = default;

        public void Init(IEcsSystems systems)
        {
            var playerEntity = systems.GetWorld().NewEntity();

            _isControllerByPlayer.Value.Add(playerEntity);

            GameObject player = Object.Instantiate(ResourceLoader.Load(
                Identifiers.PlayerPrefabName));

            UnitSettings settings = player.GetComponent<UnitSettings>();

            MovingInit(player, settings, playerEntity);
            AnimationInit(player, settings, playerEntity);
            SoundInit(player, settings, playerEntity);

            _animationService = new ControlAnimationService(systems);

            _animationService.StartAnimation(
                playerEntity, Configs.Track.idle, true, 5.0f);
        }

        private void MovingInit(
            GameObject unitGameObject, UnitSettings settings, int unitEntity)
        {
            ref var playerUnit = ref _unitPool.Value.Add(unitEntity);

            playerUnit.Settings = settings;

            unitGameObject.transform.position = new Vector3(0, 0, 0);
            playerUnit.Transform = unitGameObject.GetComponent<Transform>();
            playerUnit.RigidBody = unitGameObject.GetComponent<Rigidbody2D>();
        }

        private void AnimationInit(
            GameObject unitGameObject, UnitSettings settings, int unitEntity)
        {
            ref var unitAnimation = ref _unitAnimationPool.Value.Add(unitEntity);

            unitAnimation.SpriteRenderer = 
                unitGameObject.GetComponent<SpriteRenderer>();

            unitAnimation.AnimationConfig = settings.AnimationConfig;
        }

        private void SoundInit(GameObject unitGameObject, UnitSettings settings, int unitEntity)
        {

            ref var unitSound = ref _unitSoundComponent.Value.Add(unitEntity);
            unitSound.AudioPlayer =
                unitGameObject.GetComponent<AudioSource>();

            unitSound.SoundConfig = settings.AudioConfig;
        }
    }
}
