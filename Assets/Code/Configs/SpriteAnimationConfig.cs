using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Configs
{
    public enum AnimationTrack
    {
        idle,
        leftLegstep,
        rightLegStep,
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
        public sealed class AnimationContext
        {
            public AnimationTrack Track;
            public List<Sprite> Sprites = new List<Sprite>();

            public SoundTrack BeginAnimationSound;
            public bool IsLoopBeginAnimationSound;

            public SoundTrack EndAnimationSound;
        }

        public List<AnimationContext> Sequences = new List<AnimationContext>();
    }
}
