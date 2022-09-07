using UnityEngine;

[CreateAssetMenu(fileName = "SceneConfig",
        menuName = "Configs/SceneConfig")]
public class SceneConfig : ScriptableObject
{
    public GameObject[] Platforms = default;

    [ContextMenu("Add Platforms")]
    private void AddPlatforms()
    {
        PlatformMarker[] platforMarkers = 
            GameObject.FindObjectsOfType<PlatformMarker>();

        Platforms = new GameObject[platforMarkers.Length];
        for (int i = 0; i < platforMarkers.Length; ++i)
        {
            Platforms[i] = platforMarkers[i].gameObject;
            //var platformMarker = Platforms[i].GetComponent<PlatformMarker>();
            //GameObject.DestroyImmediate(platformMarker);
        }


    }
}
