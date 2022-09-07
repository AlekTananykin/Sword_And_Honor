
using UnityEngine;

namespace Assets.Code.Interfaces
{
    interface IPool
    {
        GameObject GetGameObject();
        void Intake(ref GameObject item);
    }
}
