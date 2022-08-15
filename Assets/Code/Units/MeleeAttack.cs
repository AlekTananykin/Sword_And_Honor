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


        public override void Attack()
        {
            var hit = Physics2D.Raycast(
                _attackRayPosition, Vector2.right, _attackDistance);

            if (null == hit.collider)
                return;

            if (!hit.collider.gameObject.TryGetComponent(out UnitAvatar target))
            {
                //int  target.Entity;
            }
        }
    }
}
