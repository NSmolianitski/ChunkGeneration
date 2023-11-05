using ChunkGeneration.Bootstrap.Abstractions;
using UnityEngine;

namespace ChunkGeneration.Bootstrap.Systems
{
    /// <summary>
    /// Система, которая отвечает за чтение ввода игрока
    /// </summary>
    public class PlayerInputSystem : IUpdateSystem
    {
        private GameData _gameData;

        public PlayerInputSystem(GameData gameData)
        {
            _gameData = gameData;
        }

        public void Run()
        {
            _gameData.PlayerXInput = Input.GetAxis(Constants.Horizontal);
            _gameData.PlayerYInput = Input.GetAxis(Constants.Vertical);
        }
    }
}