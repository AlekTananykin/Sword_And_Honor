using Leopotam.EcsLite;
using UnityEngine;

namespace Assets.Code.Interfaces
{
    interface IUnitInitService
    {
        int Initialize(string prefabPath, Vector3 position);
    }
}
