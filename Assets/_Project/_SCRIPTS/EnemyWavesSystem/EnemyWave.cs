using Gameplay;
using UnityEngine;
using UnityEngine.Playables;

namespace EnemyWavesSystem
{
    public class EnemyWave : PlayableAsset
    {
        public int wave;
        public int number;
        public float delay;
        public float radius;
        public Enemy enemyPrefab;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<EnemyWaveBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            behaviour.wave = wave;
            behaviour.number = number;
            behaviour.delay = delay;
            behaviour.enemyPrefab = enemyPrefab;
            behaviour.radius = radius;

            return playable;
        }
    }
}
