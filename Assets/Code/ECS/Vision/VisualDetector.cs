using Asserts.Code;
using UnityEngine;

class VisualDetector : MonoBehaviour
{
    [SerializeField]
    public float VisionDistance = 1.0f;

    [SerializeField]
    public Vector2 VisionRayPosition;


    public bool ToSee(bool onTheRight, ref Vector3 targetPosition)
    {
        Vector3 gameObjectPosition = gameObject.transform.position;

        Vector3 rayPosition = new Vector2(gameObjectPosition.x,
                gameObjectPosition.y) + VisionRayPosition;

        var hit = Physics2D.Raycast(rayPosition,
                onTheRight? Vector2.right: Vector2.left, 
                VisionDistance, GameLayers.PlayerLayerMask);

        if (null == hit.transform)
            return false;

        targetPosition = hit.transform.position;

        return true;
    }
}
