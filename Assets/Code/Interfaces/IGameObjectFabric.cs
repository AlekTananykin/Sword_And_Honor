using UnityEngine;

namespace Assets.Code.Interfaces
{
    public interface IGameObjectFabric
    {
        GameObject Load(string pathToPrefab);
        void Clear();
    }
}
