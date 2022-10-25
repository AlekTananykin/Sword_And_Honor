

namespace Assets.Code.Units
{
    class ThrowingAttack : UnitAttack
    {
        public override int Attack(bool toTheLeft)
        {
            return -1;
        }
    }
}
