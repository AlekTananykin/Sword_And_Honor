using Asserts.Code;
using Assets.Code.Components;
using Assets.Code.Configs;
using Assets.Code.Fabrics;
using Assets.Code.Systems.Animation;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.Player
{
    public sealed class PlayerInitSystem : IEcsInitSystem
    {
        EcsPoolInject<Unit> _units = default;
        EcsPoolInject<ControlledByPlayer> _isControllerByPlayer = default;
                
        ControlAnimationService _animationService = default;

        public void Init(IEcsSystems systems)
        {
            var playerEntity = systems.GetWorld().NewEntity();

            ref var playerUnit = ref _units.Value.Add(playerEntity);
            _isControllerByPlayer.Value.Add(playerEntity);

            GameObject player = Object.Instantiate(ResourceLoader.Load(
                Identifiers.PlayerPrefabName));

            playerUnit.Settings = player.GetComponent<UnitSettings>();

            MovingInit(player, ref playerUnit);
            AnimationInit(player, ref playerUnit);

            _animationService = new ControlAnimationService(systems);

            _animationService.StartAnimation(
                playerEntity, Configs.Track.idle, true, 5.0f);
        }

        private void MovingInit(
            GameObject unitGameObject, ref Unit unitComponent)
        {
            unitGameObject.transform.position = new Vector3(0, 0, 0);
            unitComponent.Transform = unitGameObject.GetComponent<Transform>();
            unitComponent.RigidBody = unitGameObject.GetComponent<Rigidbody2D>();
        }

        private void AnimationInit(
            GameObject unitGameObject, ref Unit unitComponent)
        {
            unitComponent.SpriteRenderer = 
                unitGameObject.GetComponent<SpriteRenderer>();
            unitComponent.AnimationConfig = 
                unitComponent.Settings.AnimationConfig;

            
        }
    }
}
