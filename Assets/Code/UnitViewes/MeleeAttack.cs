using Asserts.Code;
using System;
using UnityEngine;

namespace Assets.Code.Units
{
    public sealed class MeleeAttack : UnitAttack
    {
        [SerializeField]
        private float _attackDistance = 1.0f;

        [SerializeField]
        Vector2 _attackRayPosition;

        [SerializeField]
        private float _attackDamage;


        public override int Attack()
        {
            var hit = Physics2D.Raycast(
                new Vector2(gameObject.transform.position.x,
                gameObject.transform.position.y + 1f) + _attackRayPosition, 
                Vector2.right, _attackDistance, GameLayers.EnemyLayerMask);

            if (null != hit.collider 
                && hit.collider.gameObject.TryGetComponent(out UnitAvatar target))
                return target.Entity;

            return -1;
        }
    }
}
