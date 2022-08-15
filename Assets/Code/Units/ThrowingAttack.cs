using Leopotam.EcsLite;
using System;

namespace Assets.Code.Units
{
    class ThrowingAttack : UnitAttack
    {
        public override void Attack()
        {
            throw new NotImplementedException();
        }
    }
}
