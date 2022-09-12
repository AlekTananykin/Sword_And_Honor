using UnityEngine;

namespace Assets.Code.Configs
{
    [CreateAssetMenu(fileName = "GiftsLocationConfig",
       menuName = "Configs/GiftsLocationConfig")]
    class GiftsLocationConfig : SceneConfigBase<GiftMarker>
    {
        [ContextMenu("Add Gifts")]
        private void AddGifts()
        {
            GiftMarker[] giftMarkers =
                GameObject.FindObjectsOfType<GiftMarker>();

            AddTargets(giftMarkers);
        }
    }
}
