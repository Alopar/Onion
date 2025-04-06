using System.Collections;
using Gameplay;
using UnityEngine;
using Utility;

namespace EnemyWavesSystem
{
    [SelectionBase]
    public class Spawner : MonoBehaviour, IHasRadius
    {
        #region FIELDS INSPECTOR
        [SerializeField] private float _radius;
        #endregion

        #region FIELDS PRIVATE
        private int _currentWave;
        #endregion

        #region PROPERTIES
        public float Radius => _radius;
        #endregion

        #region METHODS PUBLIC
        public void SpawnWave(int wave, int number, float delay, Enemy prefab)
        {
            if (!Application.isPlaying) return;
            if (_currentWave >= wave) return;
            Debug.Log("Start wave: " + wave);

            _currentWave = wave;
            StartCoroutine(SpawnEnemies(number, delay, prefab));
        }
        #endregion

        #region METHODS PRIVATE
        private Vector3 GetPointOnCircleByAngle(Vector3 center, float angle, float radius)
        {
            var x = Mathf.Sin(angle * Mathf.Deg2Rad) * radius + center.x;
            var y = Mathf.Cos(angle * Mathf.Deg2Rad) * radius + center.y;
            var z = center.z;

            return new Vector3(x, y, z);
        }
        #endregion

        #region COROUTINES
        private IEnumerator SpawnEnemies(int number, float delay, Enemy prefab)
        {
            for (var i = 0; i < number; i++)
            {
                var randomAngle = Random.Range(0f, 360f);
                var spawnPoint = GetPointOnCircleByAngle(transform.position, randomAngle, _radius);
                Instantiate(prefab, spawnPoint, Quaternion.identity);

                yield return new WaitForSeconds(delay);
            }
        }
        #endregion
    }
}
