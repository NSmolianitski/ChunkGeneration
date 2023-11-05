using System.Collections.Generic;
using UnityEngine;

namespace ChunkGeneration.ChunkMap
{
    public class ChunkMap
    {
        private Dictionary<Vector2Int, ChunkView> _map = new();

        public ChunkView this[Vector2Int coordinates]
        {
            get => GetChunkView(coordinates);
            set => _map[coordinates] = value;
        }

        public ChunkNeighbour[] GetNeighbors(Vector2Int coordinates)
        {
            var neighbors = new ChunkNeighbour[8];
            var neighborsCount = 0;
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0)
                        continue;
                
                    var neighborX = coordinates.x + i;
                    var neighborY = coordinates.y + j;
                    var neighbourCoordinates = new Vector2Int(neighborX, neighborY);
                    
                    var chunkView = GetChunkView(neighbourCoordinates);
                    neighbors[neighborsCount] = new ChunkNeighbour
                    {
                        Coordinates = neighbourCoordinates,
                        ChunkView = chunkView
                    };
                    
                    ++neighborsCount;
                }
            }

            return neighbors;
        }
        
        private ChunkView GetChunkView(Vector2Int coordinates)
        {
            return _map.TryGetValue(coordinates, out var chunkView) ? chunkView : null;
        }
    }
}