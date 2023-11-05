using UnityEngine;

namespace ChunkGeneration.Bootstrap
{
    /// <summary>
    /// Класс, который содержит ссылки на сущности сцены
    /// </summary>
    public class SceneData : MonoBehaviour
    {
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public Transform ChunkParent { get; private set; }
    }
}