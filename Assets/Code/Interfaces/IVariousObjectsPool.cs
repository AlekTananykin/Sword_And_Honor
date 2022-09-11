using UnityEngine;

namespace Assets.Code.Interfaces
{
    public interface IVariousObjectsPool
    {
        GameObject GetGameObject(string prefabPath);

        void Intake(ref GameObject item);
    }
}
