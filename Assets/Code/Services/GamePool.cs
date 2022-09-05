using Assets.Code.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Services
{
    public class GamePool
    {
        public GamePool(IGameObjectFabric fabric)
        {
            _fabric = fabric;
        }

        public GameObject GetGameObject()
        {
            if (_storage.Count > 0)
                return _storage.Pop();
            
           return  _fabric.Load("");
        }

        public void Intake(GameObject item)
        {
            if (null != item)
                _storage.Push(item);
        }


        private Stack<GameObject> _storage = new Stack<GameObject>();

        private IGameObjectFabric _fabric;
    }
}
