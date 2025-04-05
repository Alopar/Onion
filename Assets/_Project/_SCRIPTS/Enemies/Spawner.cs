using NaughtyAttributes;
using UnityEngine;
using Utility;

namespace Gameplay
{
    public class Spawner : MonoBehaviour, IHasRadius
    {
        #region FIELDS INSPECTOR
        [SerializeField] private float _radius;
        [SerializeField] private EnemyMovement _enemyPrefab;
        #endregion

        #region FIELDS PRIVATE
        #endregion

        #region PROPERTIES
        public float Radius => _radius;
        #endregion

        #region UNITY CALLBACKS
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

        #region METHODS PUBLIC
        [Button]
        public void SpawnEnemy()
        {
            var randomAngle = Random.Range(0f, 360f);
            var spawnPoint = GetPointOnCircleByAngle(transform.position, randomAngle, _radius);
            Instantiate(_enemyPrefab, spawnPoint, Quaternion.identity);
        }
        #endregion
    }
}
