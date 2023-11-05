using ChunkGeneration.Bootstrap.Abstractions;

namespace ChunkGeneration.Bootstrap.Systems
{
    /// <summary>
    /// Система, которая настраивает камеру
    /// </summary>
    public class CameraInitSystem : IInitSystem
    {
        private SceneData _sceneData;
        private GameData _gameData;
        
        public CameraInitSystem(SceneData sceneData, GameData gameData)
        {
            _sceneData = sceneData;
            _gameData = gameData;
        }

        public void Init()
        {
            var camera = _sceneData.MainCamera;
            camera.transform.parent = _gameData.PlayerView.transform;
        }
    }
}