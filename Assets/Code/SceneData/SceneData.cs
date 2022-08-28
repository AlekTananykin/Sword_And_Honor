using Sccene;
using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class SceneData : MonoBehaviour
{
    public GameObject[] Platforms;

#if UNITY_EDITOR

    [ContextMenu("Find Cells")]

    void FindCells()
    {
        CellView[] Cells = FindObjectsOfType<CellView>();

        Dictionary<Vector2, CellView> cellsDictionsry =
                new Dictionary<Vector2, CellView>();

        foreach (var cell in Cells)
        {
            cellsDictionsry.Add(new Vector2(
                cell.transform.position.x, cell.transform.position.z), cell);
        }

        List<GameObject> platformsList = new List<GameObject>();
        CreatePlatforms(cellsDictionsry, platformsList);

        Platforms = platformsList.ToArray();
    }

    private void CreatePlatforms(
        Dictionary<Vector2, CellView> cellsDictionsry,
        List<GameObject> platformsList)
    {
        List<CellView> platfomrComposition = new List<CellView>();
        while (cellsDictionsry.Count > 0)
        {
            var cellEnumerator = cellsDictionsry.GetEnumerator();
            cellEnumerator.MoveNext();

            GameObject platform = new GameObject();
            platform.name = "Platform";
            platformsList.Add(platform);

            var refCell = cellEnumerator.Current.Value;
            float beginCellX = refCell.transform.position.x;
            float beginCellY = refCell.transform.position.y;

            float endCellX = beginCellX;
            float endCellY = beginCellY;

            Vector2 key = new Vector2(beginCellX, beginCellY);

            platfomrComposition.Add(refCell);

            FindCells(cellsDictionsry, platfomrComposition, refCell.XyStep,
                ref endCellX, ref endCellY);

            FindCells(cellsDictionsry, platfomrComposition, -refCell.XyStep,
                ref beginCellX, ref beginCellY);

            platform.transform.position = new Vector3((beginCellX + endCellX) / 2, 
                (beginCellY + endCellY) / 2, 0);

            AddPlatformComponents(platformsList, platform.transform);

            platformsList.Clear();

            AddCollider(platform, 
                beginCellX, beginCellY, endCellX, endCellY, refCell.XyStep);

            cellsDictionsry.Remove(key);

            cellEnumerator.Dispose();
        }
    }

    private void AddPlatformComponents(
        List<GameObject> platformsList, Transform parentTransform)
    {
        foreach (var component in platformsList)
            component.transform.parent = parentTransform;
    }

    private void AddCollider(GameObject platform,
        float beginCellX, float beginCellY, float endCellX, float endCellY, float cellSize)
    {
        var collider = platform.AddComponent<BoxCollider2D>();

        if (endCellX < beginCellX || endCellY < beginCellY)
        {
            Debug.LogError("Wrong Collider size");
        }

        collider.size = new Vector2(endCellX - beginCellX + cellSize, 
            endCellY - beginCellY + cellSize);

    }

    private void FindCells(Dictionary<Vector2, CellView> cellsDictionsry, 
        List<CellView> platformCosition,
        float xStep, ref float endCellX, ref float endCellY)
    {
        Vector2 key = new Vector2(endCellX + xStep, endCellY);

        while (cellsDictionsry.TryGetValue(key, out CellView nextCell))
        {
            endCellX = nextCell.gameObject.transform.position.x;
            endCellY = nextCell.gameObject.transform.position.y;

            platformCosition.Add(nextCell);

            cellsDictionsry.Remove(key);
            key = new Vector2(endCellX + xStep, endCellY);
        }
    }

#endif

}
