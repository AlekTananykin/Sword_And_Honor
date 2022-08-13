using Assets.Code.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Components
{
    public struct AnimationTaskComponent
    {
        public AnimationTrack Trak;
        public SpriteRenderer SpriteRenderer;
        public List<Sprite> Sprites;
        
        public bool Loop;
        public float Speed;
        public float Counter;
        public bool Sleeps;
    }
}
