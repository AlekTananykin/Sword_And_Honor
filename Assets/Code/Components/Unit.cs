
using Assets.Code.Configs;
using UnityEngine;

namespace Assets.Code.Components
{
    public struct Unit
    {
        public Transform Transform;
        public Rigidbody2D RigidBody;

        public SpriteRenderer SpriteRenderer;
        public SpriteAnimationConfig AnimationConfig;

        public AudioSource AudioPlayer;
        public SoundPlayConfig SoundConfig;

        public UnitSettings Settings;
    }
}
