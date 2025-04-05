using System;
using UnityEngine;

namespace Gameplay
{
    public class Destroyer : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private BuildingHealth _health;
        #endregion

        #region UNITY CALLBACKS
        private void OnEnable()
        {
            _health.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            _health.OnDeath -= OnDeath;
        }
        #endregion

        #region METHODS PRIVATE
        private void OnDeath()
        {
            Destroy(gameObject);
        }
        #endregion
    }
}
