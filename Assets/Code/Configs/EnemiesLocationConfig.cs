using Assets.Code.SceneData;
using UnityEngine;

namespace Assets.Code.Configs
{
    [CreateAssetMenu(fileName = "EnemiesLocationConfig",
        menuName = "Configs/EnemiesLocationConfig")]
    sealed class EnemiesLocationConfig : SceneConfigBase<EnemyMarker>
    {
        [ContextMenu("Add Enemies")]
        private void AddEnemies()
        {
            EnemyMarker[] enemyMarkers =
                GameObject.FindObjectsOfType<EnemyMarker>();

            AddTargets(enemyMarkers);
        }
    }
}
