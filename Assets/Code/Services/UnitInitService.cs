﻿
using Asserts.Code;
using Assets.Code.ECS.Components;
using Assets.Code.ECS.Components.Unit;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Code.Services
{
    public class UnitInitService: IUnitInitService
    {
        public UnitInitService(IEcsSystems systems,
            IControlAnimationService animationService, 
            IVariousObjectsPool objectsPool)
        {
            _world = systems.GetWorld();

            _unitPool = _world.GetPool<UnitComponent>();
            _transformPool = _world.GetPool<TransformComponent>();
            _unitAnimationPool = _world.GetPool<UnitAnimationComponent>();

            _unitSoundPool = _world.GetPool<UnitSoundComponent>();
            _attackPool = _world.GetPool<MelleeComponent>();
            _healthPool = _world.GetPool<HealthComponent>();

            _gameObjectPool = _world.GetPool<GameObjectComponent>();

            _unitDirection = _world.GetPool<UnitDirectionComponent>();

            _animationService = animationService;
            _objectsPool = objectsPool;
        }

        public int Initialize(string prefabPath, 
            Vector3 position)
        {
            var unitEntity = _world.NewEntity();

            GameObject unit = _objectsPool.GetGameObject(prefabPath);

            UnitAvatar unitAvatar = unit.GetComponent<UnitAvatar>();
            unitAvatar.Entity = unitEntity;

            MovingInit(unit, unitAvatar, unitEntity, ref position);
            bool unitOrientationToTheLeft = false;
            AnimationInit(unit, unitAvatar, unitEntity, ref unitOrientationToTheLeft);

            SetUnitOrientation(unitEntity, unitOrientationToTheLeft);

            SoundInit(unit, unitEntity);
            AttackFacilitiesInit(unit, unitAvatar, unitEntity);
            HealthInit(unitAvatar, unitEntity);

            GameObjectPoolInit(unit, unitEntity);

            _animationService.StartAnimation(
                unitEntity, Configs.AnimationTrack.idle, true, 
                Identifiers.UnitAnimationSpeed);

            return unitEntity;
        }

        private void SetUnitOrientation(int unitEntity, bool unitOrientationToTheLeft)
        {
            ref var unitDirection = ref _unitDirection.Add(unitEntity);
            unitDirection.ToTheLeft = unitOrientationToTheLeft;
        }

        private void MovingInit(
            GameObject unitGameObject, UnitAvatar avatar, int unitEntity, 
            ref Vector3 position)
        {
            ref var playerUnit = ref _unitPool.Add(unitEntity);

            playerUnit.Avatar = avatar;

            unitGameObject.transform.position = position;
            
            playerUnit.RigidBody = unitGameObject.GetComponent<Rigidbody2D>();

            ref var transform = ref _transformPool.Add(unitEntity);
            transform.Transform = unitGameObject.GetComponent<Transform>();
        }

        private void AnimationInit(
            GameObject unitGameObject, UnitAvatar avatar, int unitEntity, ref bool toTheLeft)
        {
            ref var unitAnimation = ref _unitAnimationPool.Add(unitEntity);

            unitAnimation.SpriteRenderer =
                unitGameObject.GetComponent<SpriteRenderer>();

            unitAnimation.AnimationConfig = avatar.AnimationConfig;

            toTheLeft = unitAnimation.SpriteRenderer.flipX; 
        }

        private void SoundInit(GameObject unitGameObject, int unitEntity)
        {
            ref var unitSound = ref _unitSoundPool.Add(unitEntity);
            unitSound.AudioPlayer =
                unitGameObject.GetComponent<AudioSource>();
        }

        private void AttackFacilitiesInit(
            GameObject unitGameObject, UnitAvatar avatar, int unitEntity)
        {
            ref var unitAtack = ref _attackPool.Add(unitEntity);

            unitAtack.Attak =
                unitGameObject.GetComponent<UnitAttack>();

            unitAtack.Attak.LayerMask = 
                GameLayers.GetTargetMaskByLayer(unitGameObject.layer);
        }

        private void HealthInit(UnitAvatar avatar, int unitEntity)
        {
            ref var unitHealth = ref _healthPool.Add(unitEntity);
            unitHealth.Health = avatar.Health;
            unitHealth.MaxHealth = avatar.MaxHealth;
        }

        private void GameObjectPoolInit(GameObject unitObject, int unitEntity)
        {
            _gameObjectPool.Add(unitEntity).Instance = unitObject;
        }

        private EcsWorld _world = default;

        private EcsPool<UnitComponent> _unitPool = default;
        private EcsPool<TransformComponent> _transformPool = default;
        private EcsPool<UnitAnimationComponent> _unitAnimationPool = default;

        private EcsPool<UnitSoundComponent> _unitSoundPool = default;
        private EcsPool<MelleeComponent> _attackPool = default;
        private EcsPool<HealthComponent> _healthPool = default;

        private EcsPool<GameObjectComponent> _gameObjectPool = default;

        private EcsPool<UnitDirectionComponent> _unitDirection = default;

        private IControlAnimationService _animationService = default;
        private IVariousObjectsPool _objectsPool = default;
        
    }
}
