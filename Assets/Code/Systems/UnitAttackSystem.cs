﻿using Assets.Code.Components;
using Assets.Code.Components.Commands;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.Systems
{
    public sealed class UnitAttackSystem : UnitSystemBase, IEcsRunSystem
    {
        private EcsFilterInject<Inc<Unit, AttackCommand>>
            _attackUnitFilter = default;

        private EcsPoolInject<AttackCommand> _attackCommandPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _attackUnitFilter.Value)
            {
                _animationService.StartAnimation(entity, Configs.Track.attack1, _isLoop, 
                    Asserts.Code.Identifiers.UnitAnimationSpeed);

                _soundService.PlaySound(entity, SoundTrack.attack1, _isLoop);
                _attackCommandPool.Value.Del(entity);
            }
        }

        private const bool _isLoop = false;
    }
}
