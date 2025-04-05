using UnityEngine;

namespace Gameplay
{
    [SelectionBase]
    public class Core : MonoBehaviour
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
            print("💥💥💥 CORE DESTROYED, GAME OVER! 💥💥💥");
        }
        #endregion
    }
}
