using UnityEngine;
using UnityEngine.Tilemaps;

namespace Iris
{
    internal sealed class GridManagementSystem : MonoBehaviour
    {
        [SerializeField]
        private Tilemap m_WalkableTilemap;

        internal bool RequestMoveToTile(Vector3 targetPosition)
        {
            return m_WalkableTilemap.HasTile(targetPosition.ToVector3Int());
        }
    }
}
