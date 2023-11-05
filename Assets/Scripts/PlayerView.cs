using UnityEngine;

namespace ChunkGeneration
{
    /// <summary>
    /// MonoBehaviour-класс игрока
    /// </summary>
    public class PlayerView : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    }
}