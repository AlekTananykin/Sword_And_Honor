using Asserts.Code;
using Assets.Code.Fabrics;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.Player
{
    public sealed class PlayerInitSystem : IEcsInitSystem
    {          
        public void Init(IEcsSystems systems)
        {
            var playerEntity =
                _unitInitService.Value.Initialize(
                    Identifiers.PlayerPrefabName, new Vector3());

            _isControllerByPlayer.Value.Add(playerEntity);

        }

        private EcsCustomInject<IUnitInitService>
          _unitInitService = default;

        private EcsPoolInject<IsControlledByPlayerComponent>
            _isControllerByPlayer = default;
    }
}
