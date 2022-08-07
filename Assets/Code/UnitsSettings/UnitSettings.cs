using Assets.Code.Configs;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class UnitSettings : MonoBehaviour
{
    [Header("Moving")]
    public float NewStepVelocitySpeed = 0.5f;
    public float StepSpeed = 1000.0f;
    public float JumpSpeed = 1000.0f;

    [Header("Animation")]
    public SpriteAnimationConfig AnimationConfig = default;

    [Header("Audio")]
    public SoundPlayConfig AudioConfig = default;

    public bool IsGrounded { get; private set; }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        IsGrounded = true;
    }

    public void OnTriggerExit2D(UnityEngine.Collider2D collision)
    {
        IsGrounded = false;
    }

}
