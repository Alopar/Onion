using System;
using UnityEngine;

namespace Gameplay
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        #region FIELDS INSPECTOR
        [SerializeField] private float _healthMax;
        #endregion

        #region FIELDS PRIVATE
        private float _healthCurrent;
        #endregion

        #region EVENTS
        public event Action<float, float> OnHealthChanged;
        public event Action OnDeath;
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            _healthCurrent = _healthMax;
        }
        #endregion

        #region METHODS PUBLIC
        public void DealDamage(float damage)
        {
            _healthCurrent -= damage;
            OnHealthChanged?.Invoke(_healthCurrent, _healthMax);
            if (_healthCurrent > 0) return;

            OnDeath?.Invoke();
        }
        #endregion
    }
}
