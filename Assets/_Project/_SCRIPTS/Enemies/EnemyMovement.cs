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
            _core = FindAnyObjectByType<Core>()?.transform;
        }

        private void Update()
        {
            if (_core == null) return;

            var direction = _core.position - transform.position;
            Move(direction);
            Rotate(direction);
        }
        #endregion

        #region METHODS PRIVATE
        private void Move(Vector3 direction)
        {
            transform.position += direction.normalized * (_speed * Time.deltaTime);
        }

        private void Rotate(Vector3 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        #endregion
    }
}
