using Asserts.Code;
using Assets.Code.Fabrics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.Player
{
    public sealed class PlayerInitSystem : IEcsInitSystem
    {
        EcsPoolInject<Unit> _units = default;
        EcsPoolInject<ControlledByPlayer> _isControllerByPlayer = default;

        public void Init(IEcsSystems systems)
        {
            var playerEntity = systems.GetWorld().NewEntity();

            ref var playerUnit = ref _units.Value.Add(playerEntity);
            _isControllerByPlayer.Value.Add(playerEntity);

            GameObject player = Object.Instantiate(ResourceLoader.Load(
                Identifiers.PlayerPrefabName));

            player.transform.position = new Vector3(0, 0, 0);
            
            playerUnit.Transform = player.GetComponent<Transform>();

        }
    }
}
