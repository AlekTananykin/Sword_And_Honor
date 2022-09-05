using Asserts.Code;
using Assets.Code.ECS.Components;
using Assets.Code.SceneData;
using Sccene;
using System.Collections.Generic;
using UnityEngine;

public sealed class SceneFragment : MonoBehaviour
{
    public GameObject[] Platforms;
    public CharacterData[] Characters;


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

    [ContextMenu("Add Dragons")]
    void AddDragons()
    {
        var avatars = FindObjectsOfType<DragonGizmos>();
        List<CharacterData> enemyDataList = new List<CharacterData>();

        for (int i = 0; i < avatars.Length; ++i)
        {
            enemyDataList.Add(new CharacterData
            {
                Position = avatars[i].transform.position,
                Type = CharacterType.Dragon
            });
        }

        Characters = enemyDataList.ToArray();

    }

    [ContextMenu("Remove All Characters")]
    void RemoveMovingChars()
    {
        Characters = null;
    }

#endif

}
