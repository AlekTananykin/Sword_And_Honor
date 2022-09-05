using Assets.Code.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Fabrics
{
    public class GameObjectsFabric: IGameObjectFabric
    {
        public GameObject Load(string pathToPrefab)
        {
            if (!_prefabsStorage.TryGetValue(pathToPrefab, 
                out GameObject originalPrefab))
            {
                originalPrefab = ResourceLoader.Load(pathToPrefab);
                if (null == originalPrefab)
                {
                    Debug.LogError(
                        "GameObjectsLoader >> Can't load resource.");
                }
            }

            return GameObject.Instantiate(originalPrefab);
        }

        public void Clear()
        {
            _prefabsStorage.Clear();
        }

        private Dictionary<string, GameObject> _prefabsStorage = 
            new Dictionary<string, GameObject>();
    }
}
