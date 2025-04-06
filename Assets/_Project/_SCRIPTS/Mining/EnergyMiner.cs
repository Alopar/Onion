using System;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    public class EnergyMiner : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private int _income;
        [SerializeField] private float _delay;
        [SerializeField] private float _multiplier;
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            StartCoroutine(EnergyProduction());
        }
        #endregion

        #region METHODS PUBLIC
        public void SetMultiplier(float value)
        {
            _multiplier = value;
        }
        #endregion

        #region COROUTINES
        private IEnumerator EnergyProduction()
        {
            while (true)
            {
                yield return new WaitForSeconds(_delay);
                ResourcesManager.Instance.IncreaseEnergy((int)(_income * _multiplier));
            }
        }
        #endregion
    }
}
