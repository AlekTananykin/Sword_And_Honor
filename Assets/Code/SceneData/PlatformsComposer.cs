using Sccene;
using System.Collections.Generic;

using UnityEngine;

namespace Assets.Code.SceneData
{
    public class PlatformsComposer
    {
        public GameObject[] ComposeCellsToPlatforms(CellView[] Cells, 
            Transform parentTransform)
        {
            Dictionary<Vector2, CellView> cellsDictionsry =
                    new Dictionary<Vector2, CellView>();

            foreach (var cell in Cells)
            {
                cellsDictionsry.Add(new Vector2(
                    cell.transform.position.x, cell.transform.position.y), cell);
            }

            List<GameObject> platformsList = new List<GameObject>();
            CreatePlatforms(cellsDictionsry, platformsList);

            foreach (var platform in platformsList)
                platform.transform.parent = parentTransform;

            return platformsList.ToArray();
        }

        private void CreatePlatforms(
            Dictionary<Vector2, CellView> cellsDictionsry,
            List<GameObject> platformsList)
        {
            int platformNumber = 0;
            List<CellView> platformsComposition = new List<CellView>();
            while (cellsDictionsry.Count > 0)
            {
                var cellEnumerator = cellsDictionsry.GetEnumerator();
                if (!cellEnumerator.MoveNext())
                    break;

                var refCell = cellEnumerator.Current.Value;
                cellsDictionsry.Remove(cellEnumerator.Current.Key);
                cellEnumerator.Dispose();

                float beginCellX = refCell.transform.position.x;
                float beginCellY = refCell.transform.position.y;

                float endCellX = beginCellX;
                float endCellY = beginCellY;

                platformsComposition.Add(refCell);

                FindCells(cellsDictionsry, platformsComposition, refCell.XyStep,
                    ref endCellX, ref endCellY);

                FindCells(cellsDictionsry, platformsComposition, -refCell.XyStep,
                    ref beginCellX, ref beginCellY);

                GameObject platform = new GameObject("Platform_" +
                    (platformNumber ++).ToString());

                platformsList.Add(platform);

                platform.transform.position = new Vector3(
                    (beginCellX + endCellX) / 2.0f,
                    (beginCellY + endCellY) / 2.0f, 0.0f);

                AddPlatformComponents(platformsComposition, platform.transform);

                platformsComposition.Clear();

                AddCollider(platform,
                    beginCellX, beginCellY, endCellX, endCellY, refCell.XyStep);
            }
        }

        private void AddPlatformComponents(
            List<CellView> platformsList, Transform parentTransform)
        {
            foreach (var component in platformsList)
            {
                component.transform.SetParent(parentTransform);
                component.enabled = false;
            }
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
    }
}
