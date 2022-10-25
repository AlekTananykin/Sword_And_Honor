using UnityEngine;

public abstract class UnitAttack : MonoBehaviour
{
    public abstract int Attack(bool toTheLeft);

    public int LayerMask { get; set; }
}
