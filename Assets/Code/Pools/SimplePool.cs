using Assets.Code.Interfaces;
using UnityEngine;

namespace Assets.Code.Pools
{
    class SimplePool: GamePool, IPool
    {
        public SimplePool(IGameObjectFabric fabric, string pathToGameObject)
            : base(fabric, pathToGameObject)
        { 
        }

        public new GameObject GetGameObject()
        {
            return base.GetGameObject();
        }

        public new void Intake(ref GameObject gameObject)
        {
            base.Intake(ref gameObject);
        }
    }
}
