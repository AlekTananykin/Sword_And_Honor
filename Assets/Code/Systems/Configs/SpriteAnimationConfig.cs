using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Configs
{
    public enum Track
    {
        idle,
        walk,
        run,
        jump,
        fall,
        attack1,
        attack2,
        attack3,
        block,
        hurt,
        death
    }

    [CreateAssetMenu (fileName ="SpriteAnimationConfig", 
        menuName ="Configs/SpriteAnimationConfig")]
    public sealed  class SpriteAnimationConfig: ScriptableObject
    {
        [Serializable]
        public sealed class SpriteSequence
        {
            public Track Track;
            public List<Sprite> Sprites = new List<Sprite>();
        }

        public List<SpriteSequence> Sequences = new List<SpriteSequence>();
    }
}
