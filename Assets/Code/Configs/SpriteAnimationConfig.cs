using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Configs
{
    public enum AnimationTrack
    {
        idle,
        run,
        walk,
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
        public sealed class AnimationFrame
        {
            public Sprite Sprite = default;
            public AudioClip AudioClip = default;
        }

        [Serializable]
        public sealed class AnimationClip
        {
            public AnimationTrack Track;
            public List<AnimationFrame> Clip = new List<AnimationFrame>();
        }

        public List<AnimationClip> Sequences = new List<AnimationClip>();
    }
}
