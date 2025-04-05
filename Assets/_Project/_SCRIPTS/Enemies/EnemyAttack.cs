using System;
using UnityEngine;

namespace Gameplay
{
    public class EnemyAttack : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 10)] private float _damage;
        [SerializeField, Range(0, 1)] private float _cooldown;

        [Space(10)]
        [SerializeField] private EnemyMovement _movement;
        #endregion

        #region FIELDS PRIVATE
        private BuildingHealth _buildingHealth;
        private float _cooldownTimer;
        #endregion

        #region UNITY CALLBACKS
        private void OnTriggerEnter2D(Collider2D other)
        {
            _buildingHealth = other.gameObject.GetComponentInParent<BuildingHealth>();
            if (_buildingHealth == null) return;
            if (gameObject.CompareTag("EnemyFly") && !_buildingHealth.CompareTag("BuildingSupport")) return;

            _movement.enabled = false;
        }

        private void LateUpdate()
        {
            if (_buildingHealth == null || !_buildingHealth.isActiveAndEnabled)
            {
                _movement.enabled = true;
                return;
            }

            Attack();
        }
        #endregion

        #region METHODS PRIVATE
        private void Attack()
        {
            if (_cooldownTimer >= Time.time) return;

            _buildingHealth.DealDamage(_damage);
            _cooldownTimer = Time.time + _cooldown;
        }
        #endregion
    }
}
