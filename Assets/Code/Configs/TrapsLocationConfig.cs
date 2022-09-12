
using UnityEngine;

namespace Assets.Code.Configs
{
    [CreateAssetMenu(fileName = "TrapsLocationConfig",
        menuName = "Configs/TrapsLocationConfig")]
    class TrapsLocationConfig: SceneConfigBase<TrapMarker>
    {
        [ContextMenu("Add Traps")]
        private void AddTraps()
        {
            TrapMarker[] trapMarkers =
                GameObject.FindObjectsOfType<TrapMarker>();

            AddTargets(trapMarkers);
        }
    }
}
