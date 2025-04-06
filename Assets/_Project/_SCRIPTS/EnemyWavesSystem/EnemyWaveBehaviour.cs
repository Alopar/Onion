using Gameplay;
using UnityEngine.Playables;

namespace EnemyWavesSystem
{
    public class EnemyWaveBehaviour : PlayableBehaviour
    {
        public int wave;
        public int number;
        public float delay;
        public Enemy enemyPrefab;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            var spawner = playerData as Spawner;
            spawner.SpawnWave(wave, number, delay, enemyPrefab);
        }
    }
}
