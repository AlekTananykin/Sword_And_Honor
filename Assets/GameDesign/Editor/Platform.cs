using Assets.Code.SceneData;
using Sccene;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public sealed class Platform
{
    [MenuItem("Sword_And_Honor/Platforms/ComposePlatforms")]
    private static void ComposeCells()
    {
        CellView[] cells = GameObject.FindObjectsOfType<CellView>();

        Dictionary<Vector2, CellView> cellsDictionary =
                new Dictionary<Vector2, CellView>();

        PlatformsComposer platformsComposer = new PlatformsComposer();
        var platforms = platformsComposer.ComposeCellsToPlatforms(cells, null);

        for (int i = 0; i < platforms.Length; ++i)
            platforms[i].AddComponent<PlatformMarker>();

        foreach (var cell in cells)
        {
            var cellViewComponent = cell.GetComponent<CellView>();
            GameObject.DestroyImmediate(cellViewComponent);
        }
    }
}
