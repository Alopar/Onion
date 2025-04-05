using NaughtyAttributes;
using UnityEngine;

namespace Gameplay
{
    public class Spawner : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private EnemyMovement _enemyPrefab;
        #endregion

        #region FIELDS PRIVATE
        #endregion

        #region UNITY CALLBACKS
        #endregion

        #region METHODS PRIVATE
        #endregion

        #region METHODS PUBLIC
        [Button]
        public void SpawnEnemy()
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        }
        #endregion
    }
}
