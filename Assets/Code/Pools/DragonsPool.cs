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

        public new void Intake(ref GameObject dragon)
        {
            base.Intake(ref dragon);
            dragon = null;
        }

        GameObject IPool.GetGameObject()
        {
            return base.GetGameObject();
        }
    }
}
