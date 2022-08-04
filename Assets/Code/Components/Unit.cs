
using Assets.Code.Configs;
using UnityEngine;

public struct Unit
{
    public Transform Transform;
    public Rigidbody2D RigidBody;

    public SpriteRenderer SpriteRenderer;

    public SpriteAnimationConfig AnimationConfig;
    
    public UnitSettings Settings;
}
