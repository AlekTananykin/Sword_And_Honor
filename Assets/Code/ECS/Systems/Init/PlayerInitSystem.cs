using Asserts.Code;
using Assets.Code.ECS.Components;
using Assets.Code.Fabrics;
using Assets.Code.Systems.Animation;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.Player
{
    public sealed class PlayerInitSystem : IEcsInitSystem
    {
        EcsPoolInject<UnitComponent> _unitPool = default;
        EcsPoolInject<UnitAnimationComponent> _unitAnimationPool = default;

        EcsPoolInject<UnitSoundComponent> _unitSoundPool = default;
        EcsPoolInject<AttackComponent> _attackPool = default;
        EcsPoolInject<HealthComponent> _healthPool = default;
      
        EcsPoolInject<IsControlledByPlayerComponent> 
            _isControllerByPlayer = default;
                
        EcsCustomInject<ControlAnimationService> 
            _animationService = default;

        public void Init(IEcsSystems systems)
        {
            var playerEntity = systems.GetWorld().NewEntity();

            _isControllerByPlayer.Value.Add(playerEntity);

            GameObject player = Object.Instantiate(ResourceLoader.Load(
                Identifiers.PlayerPrefabName));

            UnitAvatar unitAvatar = player.GetComponent<UnitAvatar>();
            unitAvatar.Entity = playerEntity;


            MovingInit(player, unitAvatar, playerEntity);
            AnimationInit(player, unitAvatar, playerEntity);
            SoundInit(player, unitAvatar, playerEntity);
            AttackFacilitiesInit(player, unitAvatar, playerEntity);
            HealthInit(unitAvatar, playerEntity);

            _animationService.Value.StartAnimation(
                playerEntity, Configs.AnimationTrack.idle, true, 5.0f);
        }

        private void MovingInit(
            GameObject unitGameObject, UnitAvatar settings, int unitEntity)
        {
            ref var playerUnit = ref _unitPool.Value.Add(unitEntity);

            playerUnit.Avatar = settings;

            unitGameObject.transform.position = new Vector3(0, 0, 0);
            playerUnit.Transform = unitGameObject.GetComponent<Transform>();
            playerUnit.RigidBody = unitGameObject.GetComponent<Rigidbody2D>();
        }

        private void AnimationInit(
            GameObject unitGameObject, UnitAvatar settings, int unitEntity)
        {
            ref var unitAnimation = ref _unitAnimationPool.Value.Add(unitEntity);

            unitAnimation.SpriteRenderer = 
                unitGameObject.GetComponent<SpriteRenderer>();

            unitAnimation.AnimationConfig = settings.AnimationConfig;
        }

        private void SoundInit(
            GameObject unitGameObject, UnitAvatar settings, int unitEntity)
        {
            ref var unitSound = ref _unitSoundPool.Value.Add(unitEntity);
            unitSound.AudioPlayer =
                unitGameObject.GetComponent<AudioSource>();
        }

        private void AttackFacilitiesInit(
            GameObject unitGameObject, UnitAvatar settings, int unitEntity)
        {
            ref var unitAtack = ref _attackPool.Value.Add(unitEntity);

            unitAtack.Attak =
                unitGameObject.GetComponent<UnitAttack>();
        }

        private void HealthInit(UnitAvatar avatart, int unitEntity)
        {
            ref var unitHealth = ref _healthPool.Value.Add(unitEntity);
            unitHealth.Health = avatart.Health;
        }
    }
}
