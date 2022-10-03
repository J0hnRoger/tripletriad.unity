using UnityEngine;

namespace TripleTriad.Battle
{
    public class PositionUtils : MonoBehaviour
    {
        public static Vector3 ScreenToWorldPosition(Camera camera, Vector3 position)
        {
            position.z = camera.nearClipPlane;
            return camera.ScreenToWorldPoint(position);
        }
    }
}

