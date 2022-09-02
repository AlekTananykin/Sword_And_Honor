using Assets.Code.SceneData;
using Sccene;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct EnemyData
{
    public Vector3 Position;
    public string PrefabName;
}

public sealed class SceneFragment : MonoBehaviour
{
    public GameObject[] Platforms;

    public EnemyData[] EnemyData;


#if UNITY_EDITOR

    [ContextMenu("Compose Ground Cells")]
    void ComposeCells()
    {
        CellView[] cells = FindObjectsOfType<CellView>();

        Dictionary<Vector2, CellView> cellsDictionary =
                new Dictionary<Vector2, CellView>();

        PlatformsComposer platformsComposer = new PlatformsComposer();
        Platforms = 
            platformsComposer.ComposeCellsToPlatforms(cells, this.transform);

    }

    [ContextMenu("Decompose platforms")]
    void DecomposeGroundCells()
    {
        for (int i = 0; i < Platforms.Length; ++ i)
        {
            var platform = Platforms[i];
            CellView[] children = platform.GetComponentsInChildren<CellView>();

            for (int childCounter = 0; childCounter < children.Length; ++childCounter)
            {
                var child = children[childCounter];
                child.enabled = true;
                child.gameObject.transform.parent = null;
            }
            DestroyImmediate(platform);
        }

        Platforms = null;
    }

    [ContextMenu("Add Moving chars")]
    void AddAllEnemies()
    {
        var avatars = FindObjectsOfType<UnitAvatar>();
        List<EnemyData> enemyDataList = new List<EnemyData>();

        for (int i = 0; i < avatars.Length; ++i)
        {
            enemyDataList.Add(new EnemyData { 
                Position = avatars[i].transform.position,
                //PrefabName = avatars[i].
            });
        }

        EnemyData = enemyDataList.ToArray();

    }

    [ContextMenu("Remove All moving chars")]
    void RemoveMovingChars()
    {
        
    }

#endif

}
