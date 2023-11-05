using System.Collections.Generic;

namespace ChunkGeneration.Bootstrap
{
    /// <summary>
    /// Класс, который содержит данные текущей игровой сессии
    /// </summary>
    public class GameData
    {
        public PlayerView PlayerView;
        public float PlayerXInput;
        public float PlayerYInput;

        public ChunkView CurrentChunk;
        public readonly List<ChunkView> EnabledChunks = new();
        public readonly ChunkMap.ChunkMap ChunkMap = new();
    }
}