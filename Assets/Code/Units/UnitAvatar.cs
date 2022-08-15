using Assets.Code.Configs;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(AudioSource))]
public class UnitAvatar : MonoBehaviour
{
    [Header("Moving")]
    public float NewStepVelocitySpeed = 0.5f;
    public float StepSpeed = 1000.0f;
    public float JumpSpeed = 1000.0f;

    [Header("Health")]
    public float Health = 100.0f;

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

    public int Entity
    {
        set;
        get;
    }

    public float GetRenderFlipX()
    {
        var renderer = gameObject.GetComponent<SpriteRenderer>();
        float look = renderer.flipX ? 1.0f : -1.0f;
        Destroy(renderer);
        return look;
    }

}
