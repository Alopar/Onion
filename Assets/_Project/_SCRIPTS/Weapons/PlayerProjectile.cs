using System;
using UnityEngine;

namespace Gameplay
{
    public class PlayerProjectile : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private float _speed;
        #endregion

        #region FIELDS PRIVATE
        private float _damage;
        #endregion

        #region UNITY CALLBACKS
        private void OnCollisionEnter2D(Collision2D other)
        {
            var enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth>();
            if (enemyHealth == null) return;

            enemyHealth.DealDamage(_damage);
            Destroy(gameObject);
        }

        private void Update()
        {
            transform.position += transform.right * (_speed * Time.deltaTime);
        }
        #endregion

        #region METHODS PRIVATE
        #endregion

        #region METHODS PUBLIC
        public void Init(float damage)
        {
            _damage = damage;
            Destroy(gameObject, 3f);
        }
        #endregion
    }
}
