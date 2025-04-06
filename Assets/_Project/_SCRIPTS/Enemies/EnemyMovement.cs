using UnityEngine;

namespace Gameplay
{
    public class EnemyMovement : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 10)] private float _speed = 3f;

        [Space(10)]
        [SerializeField] private Transform _view;
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
            switch (direction.x)
            {
                case < 0: _view.rotation = Quaternion.Euler(0f, 180f, 0f);
                    break;
                default: _view.rotation = Quaternion.Euler(0f, 0f, 0f);
                    break;
            }
        }
        #endregion
    }
}
