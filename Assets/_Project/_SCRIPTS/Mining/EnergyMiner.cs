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
        #endregion

        public void SetIncome(int income) =>
            _income = income;

        public void SetCooldown(float cooldown) =>
            _delay = cooldown;

        #region UNITY CALLBACKS
        private void Start()
        {
            StartCoroutine(EnergyProduction());
        }
        #endregion

        #region COROUTINES
        private IEnumerator EnergyProduction()
        {
            while (true)
            {
                yield return new WaitForSeconds(_delay);
                ResourcesManager.Instance.IncreaseEnergy(_income);
            }
        }
        #endregion
    }
}
