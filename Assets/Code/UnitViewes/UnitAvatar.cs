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
    public float StepSpeed = 7000.0f;
    public float JumpSpeed = 10000.0f;

    [Header("Attack")]
    public float DamageSize = 5.0f;

    [Header("Health"), Range(0, 100)]
    public int Health = 0;

    [Header("Max Health"), Range(0, 1000)]
    public int MaxHealth = 100;

    [Header("Animation")]
    public SpriteAnimationConfig AnimationConfig = default;

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
