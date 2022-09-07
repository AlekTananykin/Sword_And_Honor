using Asserts.Code;
using Assets.Code.Interfaces;
using UnityEngine;

namespace Assets.Code.Pools
{
    class DragonsPool: GamePool, IPool
    {
        public DragonsPool(IGameObjectFabric fabric)
            :base(fabric, PrefabPathes.DragonPrefabPath)
        { 
        }

        public GameObject Create()
        {
            return base.GetGameObject();
        }

        public void Intake(ref GameObject dragon)
        {
            base.Intake(dragon);
            dragon = null;
        }

        GameObject IPool.GetGameObject()
        {
            throw new System.NotImplementedException();
        }
    }
}
