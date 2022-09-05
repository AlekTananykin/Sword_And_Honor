using Asserts.Code;
using Assets.Code.Interfaces;
using UnityEngine;

namespace Assets.Code.Pools
{
    class DragonsPool: GamePool
    {
        public DragonsPool(IGameObjectFabric fabric)
            :base(fabric, PrefabPathes.GragonPrefabPath)
        { 
        }

        public GameObject CreateDragon()
        {
            return GetGameObject();
        }

        public void Intake(ref GameObject dragon)
        {
            base.Intake(dragon);
            dragon = null;
        }
    }
}
