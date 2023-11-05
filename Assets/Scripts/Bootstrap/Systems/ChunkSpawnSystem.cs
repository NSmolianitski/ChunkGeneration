using System.Linq;
using ChunkGeneration.Bootstrap.Abstractions;
using ChunkGeneration.Extensions;
using ChunkGeneration.Signals;
using UnityEngine;

namespace ChunkGeneration.Bootstrap.Systems
{
    /// <summary>
    /// Система, которая отвечает за спавн чанка
    /// </summary>
    public class ChunkSpawnSystem : IInitSystem
    {
        private GameConfig _config;
        private SceneData _sceneData;
        private GameData _gameData;

        private Vector2Int _oldPlayerCoordinates;
        private bool _firstSpawn = true;
        
        public ChunkSpawnSystem(GameConfig config, SceneData sceneData, GameData gameData)
        {
            _config = config;
            _sceneData = sceneData;
            _gameData = gameData;
        }

        public void Init()
        {
            SpawnChunk(Vector3.zero, new Vector2Int(0, 0));
            Supyrb.Signals.Get<PlayerTriggeredChunkSignal>().AddListener(OnPlayerTriggeredChunk);
        }

        /// <summary>
        /// Обрабатывает появление игрока в чанке: находит соседние чанки и включает их,
        /// спавнит новые при необходимости, выключает дальние
        /// </summary>
        /// <param name="enteredChunk"></param>
        private void OnPlayerTriggeredChunk(ChunkView enteredChunk)
        {
            var neighbors = _gameData.ChunkMap.GetNeighbors(enteredChunk.Coordinates);
            foreach (var neighbor in neighbors)
            {
                if (neighbor.ChunkView == null)
                    SpawnChunk(GetWorldPointByMapCoordinatesDiff(neighbor.Coordinates), neighbor.Coordinates);
                else
                    neighbor.ChunkView.gameObject.SetActive(true);
            }

            if (!_firstSpawn)
                DisableFarChunks(enteredChunk.Coordinates);
            else
                _firstSpawn = false;
        }

        /// <summary>
        /// Отключает дальние чанки в зависимости от направления игрока
        /// </summary>
        /// <param name="currentChunkCoordinates"></param>
        private void DisableFarChunks(Vector2Int currentChunkCoordinates)
        {
            var direction = _oldPlayerCoordinates - currentChunkCoordinates;

            var swappedDirection = Utility.Utility.SwapCoordinates(direction);
            var cornerChunkCoordinates = direction + _oldPlayerCoordinates - swappedDirection;

            for (var i = 0; i < 3; ++i)
            {
                var chunk = _gameData.ChunkMap[cornerChunkCoordinates];
                if (chunk != null)
                    chunk.gameObject.SetActive(false);
                
                cornerChunkCoordinates += swappedDirection;
            }
            
            _oldPlayerCoordinates = currentChunkCoordinates;
        }

        /// <summary>
        /// Возвращает точку в мире по координатам
        /// </summary>
        /// <param name="targetCoordinates"></param>
        /// <returns></returns>
        private Vector3 GetWorldPointByMapCoordinatesDiff(Vector2Int targetCoordinates)
        {
            return new Vector3(targetCoordinates.x * _config.ChunkSize, 0, targetCoordinates.y * _config.ChunkSize);
        }

        /// <summary>
        /// Создаёт новый чанк в определённой точке, добавляет координаты из хранилища чанков и заполняет объектами
        /// </summary>
        /// <param name="position"></param>
        /// <param name="coordinates"></param>
        private void SpawnChunk(Vector3 position, Vector2Int coordinates)
        {
            var chunk = Object.Instantiate(_config.ChunkPrefab, position, Quaternion.identity, _sceneData.ChunkParent);
            chunk.GroundMeshRenderer.material = _config.ChunkFloorMaterial.GetRandom();
            SpawnChunkFillObjects(chunk.transform, _config.ChunkObstaclePoints, _config.ChunkObstacles);
            SpawnChunkFillObjects(chunk.transform, _config.ChunkDecorativePoints, _config.ChunkDecorativeObjects);
            SpawnChunkWalls(chunk);
            chunk.Coordinates = coordinates;

            _gameData.ChunkMap[coordinates] = chunk;
            _gameData.EnabledChunks.Add(chunk);
        }

        /// <summary>
        /// Создаёт случайные декоративные объекты и препятствия в случайных точках
        /// </summary>
        /// <param name="chunkTransform"></param>
        /// <param name="points"></param>
        /// <param name="prefabs"></param>
        private void SpawnChunkFillObjects(Transform chunkTransform, Vector3[] points, GameObject[] prefabs)
        {
            var count = Random.Range(_config.MinObstacleCount, _config.MaxObstacleCount + 1);
            var pointsListCopy = points.ToList();
            for (var i = 0; i < count; ++i)
            {
                var point = pointsListCopy.GetRandom();
                pointsListCopy.Remove(point);

                var prefab = prefabs.GetRandom();
                var randomRotation = Quaternion.Euler(0, Random.Range(0, 360f), 0);
                Object.Instantiate(prefab, chunkTransform.position + point, randomRotation, chunkTransform);
            }
        }

        /// <summary>
        /// Генерирует случайное количество стен
        /// </summary>
        /// <param name="chunkView">Чанк, для которого нужно создать стены</param>
        private void SpawnChunkWalls(ChunkView chunkView)
        {
            for (var i = 0; i < 4; ++i)
            {
                var chunkTransform = chunkView.transform;
                var spawnPoint = _config.ChunkBoundSpawnPoints[i] + chunkTransform.position;
                var random = Random.Range(0, 100);
                var rotation = (i % 2 == 0) ? Quaternion.identity : Quaternion.Euler(0, 90, 0);
                
                if (random < _config.WallSpawnChance)
                    Object.Instantiate(_config.ChunkHorizontalWallPrefab, spawnPoint, rotation, chunkTransform);
            }
        }
    }
}