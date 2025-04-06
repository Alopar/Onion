using UnityEngine.Timeline;

namespace EnemyWavesSystem
{
    [TrackColor(0.85f, 0.25f, 1f)]
    [TrackBindingType(typeof(Spawner))]
    [TrackClipType(typeof(EnemyWave))]
    public class SpawnTrack : TrackAsset
    {
        
    }
}
