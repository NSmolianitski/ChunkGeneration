using ChunkGeneration.Bootstrap.Abstractions;
using UnityEngine;

namespace ChunkGeneration.Bootstrap.Systems
{
    /// <summary>
    /// Система, которая отвечает за передвижение игрока
    /// </summary>
    public class PlayerMoveSystem : IFixedUpdateSystem
    {
        private GameData _gameData;
        private GameConfig _config;
        private Rigidbody _playerRigidbody;
        
        public PlayerMoveSystem(GameData gameData, GameConfig config)
        {
            _gameData = gameData;
            _config = config;
            _playerRigidbody = gameData.PlayerView.Rigidbody;
        }
        
        public void Run()
        {
            var direction = new Vector3(_gameData.PlayerXInput, 0, _gameData.PlayerYInput);
            _playerRigidbody.velocity = direction * _config.PlayerSpeed;
        }
    }
}