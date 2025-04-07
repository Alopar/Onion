using UnityEngine;
using UnityEngine.Events;

namespace Gameplay
{
    [SelectionBase]
    public class Core : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private BuildingHealth _health;

        [Space(10)]
        [SerializeField] private UnityEvent _onDeath;
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
            _onDeath.Invoke();
            print("💥💥💥 CORE DESTROYED, GAME OVER! 💥💥💥");
        }
        #endregion
    }
}
