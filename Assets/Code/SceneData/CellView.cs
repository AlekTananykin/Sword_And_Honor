using UnityEngine;


namespace Sccene
{
#if UNITY_EDITOR
    [ExecuteAlways]
    [SelectionBase]
#endif
    public sealed class CellView : MonoBehaviour
    {
        public Transform Transform;
        public float XyStep = 1.0f;

        void Awaike() 
        {
            Transform = transform;
        }


#if UNITY_EDITOR

        void Update()
        {
            if (Application.isPlaying || !Transform.hasChanged)
                return;

            var newPosition = Vector3.zero;
            var curPosition = Transform.localPosition;

            newPosition.x = Mathf.RoundToInt(curPosition.x / XyStep) * XyStep;
            newPosition.y = Mathf.RoundToInt(curPosition.y / XyStep) * XyStep;

            Transform.localPosition = newPosition;
        }
#endif
    }
}