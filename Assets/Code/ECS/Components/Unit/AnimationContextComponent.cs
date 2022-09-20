using Assets.Code.Configs;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Code.Configs.SpriteAnimationConfig;

namespace Assets.Code.ECS.Components
{
    public struct AnimationContextComponent
    {
        public AnimationTrack Trak;
        public SpriteRenderer SpriteRenderer;

        public List<AnimationFrame> Clip;
        
        public bool Loop;
        public float Speed;
        public float Counter;
        public bool Sleeps;
    }
}
