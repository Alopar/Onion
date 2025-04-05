using UnityEngine;

namespace Gameplay
{
    public class EnemyMovement : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 10)] private float _speed = 3f;
        #endregion

        #region FIELDS PRIVATE
        private Transform _core;
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            _core = FindAnyObjectByType<Core>().transform;
        }

        private void Update()
        {
            if (_core == null) return;
            Move(_core.position - transform.position);
        }
        #endregion

        #region METHODS PRIVATE
        private void Move(Vector3 direction)
        {
            transform.Translate(direction.normalized * (_speed * Time.deltaTime));
        }
        #endregion
    }
}
