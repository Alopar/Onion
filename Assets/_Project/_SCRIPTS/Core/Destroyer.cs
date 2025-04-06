using System;
using UnityEngine;

namespace Gameplay
{
    public class Destroyer : MonoBehaviour
    {
        #region FIELDS PRIVATE
        private IHealth _health;
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            _health = gameObject.GetComponent<IHealth>();
        }

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
