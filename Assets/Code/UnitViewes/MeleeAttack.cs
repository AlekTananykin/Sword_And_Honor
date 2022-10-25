using UnityEngine;

namespace Assets.Code.Units
{
    public sealed class MeleeAttack : UnitAttack
    {
        [SerializeField]
        public float AttackDistance = 1.0f;

        [SerializeField]
        public Vector2 AttackRayPosition;

        [SerializeField]
        public float AttackDamage;


        public override int Attack(bool toTheLeft)
        {
            var hit = Physics2D.Raycast(
                new Vector2(gameObject.transform.position.x,
                gameObject.transform.position.y + 1f) + AttackRayPosition,
                toTheLeft ? Vector2.left: Vector2.right, 
                AttackDistance, LayerMask);

            if (null != hit.collider 
                && hit.collider.gameObject.TryGetComponent(out UnitAvatar target))
                return target.Entity;

            return -1;
        }
    }
}
