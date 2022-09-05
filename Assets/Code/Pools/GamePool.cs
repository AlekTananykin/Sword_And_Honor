using Assets.Code.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Pools
{
    public abstract class GamePool
    {
        protected GamePool(IGameObjectFabric fabric, string pathToGameObject)
        {
            _fabric = fabric;
            _pathToGameObject = pathToGameObject;
        }

        protected GameObject GetGameObject()
        {
            if (_storage.Count > 0)
                return _storage.Pop();
            
           return  _fabric.Load(_pathToGameObject);
        }

        protected void Intake(GameObject item)
        {
            if (null != item)
                _storage.Push(item);
        }

        private Stack<GameObject> _storage = new Stack<GameObject>();

        private readonly IGameObjectFabric _fabric;
        private readonly string _pathToGameObject;
    }
}
