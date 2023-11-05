using ChunkGeneration.Bootstrap.Abstractions;
using UnityEngine;

namespace ChunkGeneration.Bootstrap.Systems
{
    /// <summary>
    /// Система, которая отвечает за спавн игрока
    /// </summary>
    public class PlayerSpawnSystem : IInitSystem
    {
        private GameData _gameData;
        private GameConfig _config;

        public PlayerSpawnSystem(GameData gameData, GameConfig config)
        {
            _gameData = gameData;
            _config = config;
        }

        public void Init()
        {
            _gameData.PlayerView = Object.Instantiate(_config.PlayerPrefab);
        }
    }
}