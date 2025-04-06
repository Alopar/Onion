using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerAttack : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 999)] private float _damage;
        [SerializeField, Range(0, 99)] private float _cooldown;

        [Space(10)]
        [SerializeField] private Transform _firePoint;
        [SerializeField] private PlayerProjectile _projectilePrefab;

        [Space(10)]
        [SerializeField] private InputActionReference _fireInputAction;
        [SerializeField] private InputActionReference _lookInputAction;
        #endregion

        #region FIELDS PRIVATE
        private Camera _camera;
        private float _cooldownTimer;
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (!_fireInputAction.action.IsPressed()) return;
            Attack();
        }
        #endregion

        #region METHODS PRIVATE
        private void Attack()
        {
            if (_cooldownTimer >= Time.time) return;

            _cooldownTimer = Time.time + _cooldown;
            var projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
            Rotate(projectile.transform, GetLookDirection());
            projectile.Init(_damage);
        }

        private void Rotate(Transform projectile, Vector3 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private Vector2 GetLookDirection()
        {
            var worldPoint = _camera.WorldToScreenPoint(_firePoint.position);
            var screenPosition = new Vector2(worldPoint.x, worldPoint.y);
            var mousePosition = _lookInputAction.action.ReadValue<Vector2>();

            return (mousePosition - screenPosition).normalized;;
        }
        #endregion
    }
}
