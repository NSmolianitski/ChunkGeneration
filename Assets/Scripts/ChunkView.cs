using ChunkGeneration.Signals;
using UnityEngine;

namespace ChunkGeneration
{
    /// <summary>
    /// Класс, который содержит ссылки на компоненты чанка и отслеживает триггер игрока
    /// </summary>
    public class ChunkView : MonoBehaviour
    {
        [field: SerializeField] public MeshRenderer GroundMeshRenderer { get; private set; }

        public Vector2Int Coordinates { get; set; }

        private static readonly PlayerTriggeredChunkSignal TriggerSignal =
            Supyrb.Signals.Get<PlayerTriggeredChunkSignal>();
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerView>())
                return;
            
            TriggerSignal.Dispatch(this);   
        }
    }
}