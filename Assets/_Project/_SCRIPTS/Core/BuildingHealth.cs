using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    public class BuildingHealth : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private float _healthMax;

        [Space(10)]
        [SerializeField] private UnityEvent _onDie;
        #endregion

        #region FIELDS PRIVATE
        private float _healthCurrent;
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
            if (_healthCurrent > 0) return;

            _onDie.Invoke();
        }
        #endregion
    }
}
