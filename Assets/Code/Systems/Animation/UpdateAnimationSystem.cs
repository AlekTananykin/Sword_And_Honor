﻿using Assets.Code.Components;
using Assets.Code.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Assets.Code.Systems.Animation
{
    internal class UpdateAnimationSystem: IEcsRunSystem 
    {
        private EcsPoolInject<UnitAnimationComponent> 
            _unitAnimationPool = default;

        private EcsFilterInject<Inc<UnitAnimationComponent>> 
            _unitAnimationFilter = default;

        private readonly EcsCustomInject<TimeService> _timeService = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var animationEntity in _unitAnimationFilter.Value)
            {
                ref var animationUnit = 
                    ref _unitAnimationPool.Value.Get(animationEntity);

                UpdateAnimation(
                    _timeService.Value.DeltaTime, ref animationUnit);

                animationUnit.SpriteRenderer.sprite = 
                    animationUnit.Sprites[(int)animationUnit.Counter];
            }
        }

        private void UpdateAnimation(
            float deltaTime, ref UnitAnimationComponent unitAnimation)
        {
            if (unitAnimation.Sleeps)
                return;

            unitAnimation.Counter += deltaTime * unitAnimation.Speed;

            if (unitAnimation.Loop)
            {
                while (unitAnimation.Counter > unitAnimation.Sprites.Count)
                    unitAnimation.Counter -= unitAnimation.Sprites.Count;
            }
            else if (unitAnimation.Counter > unitAnimation.Sprites.Count)
            {
                unitAnimation.Counter = unitAnimation.Sprites.Count - 1;
                unitAnimation.Sleeps = true;
            }
        }
    }
}
