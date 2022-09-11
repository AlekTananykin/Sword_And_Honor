using Assets.Code.ECS.Components;
using System;
using System.Collections;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneConfig",
        menuName = "Configs/SceneConfig")]
public class SceneConfig : ScriptableObject, IEnumerable, IEnumerator
{
    public GameObjectData this[int index]
    {
        get
        {
            return _platforms[index];
        }
    }

    public int Count => _platforms == default? 0: _platforms.Length;

    public object Current => throw new NotImplementedException();

    [SerializeField]
    private GameObjectData[] _platforms = default;

    [ContextMenu("Add Platforms")]
    private void AddPlatforms()
    {
        PlatformMarker[] platforMarkers = 
            GameObject.FindObjectsOfType<PlatformMarker>();

        _platforms = new GameObjectData[platforMarkers.Length];
        for (int i = 0; i < platforMarkers.Length; ++i)
        {
            _platforms[i].Position = 
                platforMarkers[i].gameObject.transform.position;
            _platforms[i].Path = 
                CreatePath(platforMarkers[i].gameObject.transform.name);
        }
    }

    private string CreatePath(string name)
    {
        int subPos = name.IndexOf('_');
        StringBuilder pathBuilder = new StringBuilder();

        if (subPos < 0)
        {
            pathBuilder.Append(name);
        }
        else
        {
            pathBuilder.Append(name.Substring(0, subPos));
        }
        pathBuilder.Append("s/");
        pathBuilder.Append(name);
        return pathBuilder.ToString();
    }

    public IEnumerator GetEnumerator()
    {
        return this;
    }

    public bool MoveNext()
    {
        return false;
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }
}
