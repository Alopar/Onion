using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class EnemyAttack : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 999)] private float _damage;
        [SerializeField, Range(0, 99)] private float _cooldown;

        [Space(10)]
        [SerializeField] private EnemyMovement _movement;

        [Space(10)]
        [Header("✨ Attack Feedback ✨")]
        [SerializeField] private List<AudioClip> _soundEffects;
        #endregion

        #region FIELDS PRIVATE
        private BuildingHealth _buildingHealth;
        private float _cooldownTimer;
        #endregion

        #region UNITY CALLBACKS
        private void OnTriggerEnter2D(Collider2D other)
        {
            var buildingHealth = other.gameObject.GetComponentInParent<BuildingHealth>();
            if (buildingHealth == null) return;
            if (gameObject.CompareTag("EnemyFly") && !buildingHealth.CompareTag("BuildingSupport")) return;

            _buildingHealth = buildingHealth;
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

            var clip = _soundEffects[Random.Range(0, _soundEffects.Count)];
            SoundManager.Instance.PlaySound(clip);
        }
        #endregion
    }
}
