using UnityEngine;

namespace ChunkGeneration.Utility
{
    public static class Utility
    {
        public static Vector2Int SwapCoordinates(Vector2Int coordinates)
        {
            return new Vector2Int(coordinates.y, coordinates.x);
        }
    }
}