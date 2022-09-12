using Assets.Code.Configs;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneConfig",
        menuName = "Configs/SceneConfig")]
sealed class PlatformsLocationConfig : 
    SceneConfigBase<PlatformMarker>
{
    [ContextMenu("Add Platforms")]
    private void AddPlatforms()
    {
        PlatformMarker[] platforMarkers = 
            GameObject.FindObjectsOfType<PlatformMarker>();

        AddTargets(platforMarkers);
    }
}
