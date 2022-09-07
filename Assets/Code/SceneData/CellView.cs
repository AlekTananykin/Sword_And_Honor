using UnityEngine;


namespace Sccene
{
#if UNITY_EDITOR
    [ExecuteAlways]
    [SelectionBase]
#endif
    public sealed class CellView : MonoBehaviour
    {

#if UNITY_EDITOR

        public float XyStep = 1.0f;
        void Update()
        {
            if (Application.isPlaying || !transform.hasChanged)
                return;

            var newPosition = Vector3.zero;
            var curPosition = transform.localPosition;

            newPosition.x = Mathf.RoundToInt(curPosition.x / XyStep) * XyStep;
            newPosition.y = Mathf.RoundToInt(curPosition.y / XyStep) * XyStep;

            transform.localPosition = newPosition;
        }
#endif
    }
}