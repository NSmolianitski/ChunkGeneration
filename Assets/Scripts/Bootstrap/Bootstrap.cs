using System.Collections.Generic;
using ChunkGeneration.Bootstrap.Abstractions;
using ChunkGeneration.Bootstrap.Systems;
using UnityEngine;

namespace ChunkGeneration.Bootstrap
{
    /// <summary>
    /// Упрощённый игровой менеджер. Инициализирует системы, управляет основным циклом игры
    /// </summary>
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private SceneData sceneData;

        private List<IInitSystem> _initSystems;
        private List<IUpdateSystem> _updateSystems;
        private List<IFixedUpdateSystem> _fixedUpdateSystems;
        
        private readonly GameData _gameData = new();
        
        private void Awake()
        {
            // Supyrb.Signals.Clear();
            
            _initSystems = new List<IInitSystem>
            {
                new PlayerSpawnSystem(_gameData, gameConfig),
                new CameraInitSystem(sceneData, _gameData),
                new ChunkSpawnSystem(gameConfig, sceneData, _gameData)
            };
            
            foreach (var system in _initSystems)
                system.Init();
            
            _updateSystems = new List<IUpdateSystem>
            {
                new PlayerInputSystem(_gameData)
            };

            _fixedUpdateSystems = new List<IFixedUpdateSystem>()
            {
                new PlayerMoveSystem(_gameData, gameConfig)
            };
        }

        private void Update()
        {
            foreach (var system in _updateSystems)
                system.Run();
        }
        
        private void FixedUpdate()
        {
            foreach (var system in _fixedUpdateSystems)
                system.Run();
        }
    }
}