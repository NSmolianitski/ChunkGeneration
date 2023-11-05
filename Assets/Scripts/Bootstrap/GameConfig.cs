using UnityEngine;

namespace ChunkGeneration.Bootstrap
{
    /// <summary>
    /// Файл конфигурации игры. Содержит основные настройки и префабы
    /// </summary>
    [CreateAssetMenu(fileName = "Config", menuName = "Data/Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: Header("Player Settings")]
        [field: SerializeField] public PlayerView PlayerPrefab { get; private set; }
        [field: SerializeField] public float PlayerSpeed { get; private set; }

        [field: Header("Chunk Settings")]
        [field: SerializeField, Range(1, 100)] public int WallSpawnChance { get; private set; } = 30;
        [field: SerializeField] public int MinObstacleCount { get; private set; } = 0;
        [field: SerializeField] public int MaxObstacleCount { get; private set; } = 5;
        [field: SerializeField] public int MinDecorativeObjectCount { get; private set; } = 0;
        [field: SerializeField] public int MaxDecorativeObjectCount { get; private set; } = 8;

        [field: SerializeField] public Vector3[] ChunkDecorativePoints { get; private set; }
        [field: SerializeField] public Vector3[] ChunkObstaclePoints { get; private set; }
        [field: SerializeField] public GameObject[] ChunkObstacles { get; private set; }
        [field: SerializeField] public GameObject[] ChunkDecorativeObjects { get; private set; }
        [field: SerializeField] public Material[] ChunkFloorMaterial { get; private set; }
        [field: SerializeField] public float ChunkSize { get; private set; } = 40;
        [field: SerializeField] public ChunkView ChunkPrefab { get; private set; }
        [field: SerializeField] public GameObject ChunkHorizontalWallPrefab { get; private set; }
        [field: SerializeField] public Vector3[] ChunkBoundSpawnPoints { get; private set; }
    }
}