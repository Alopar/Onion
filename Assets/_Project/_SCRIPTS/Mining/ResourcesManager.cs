using System;
using UnityEngine;

namespace Gameplay
{
    public class ResourcesManager : MonoBehaviour
    {
        #region FIELDS PRIVATE
        private static ResourcesManager _instance;
        private int _energyCounter;
        #endregion

        #region PROPERTIES
        public static ResourcesManager Instance => _instance;
        public int Energy => _energyCounter;
        #endregion

        #region EVENTS
        public event Action<int> OnEnergyChanged;
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void Start()
        {
            OnEnergyChanged?.Invoke(_energyCounter);
        }
        #endregion

        #region METHODS PUBLIC
        public void IncreaseEnergy(int amount)
        {
            _energyCounter += amount;
            OnEnergyChanged?.Invoke(_energyCounter);
        }

        public bool TryDecreaseEnergy(int amount)
        {
            if (_energyCounter < amount) return false;

            _energyCounter -= amount;
            OnEnergyChanged?.Invoke(_energyCounter);

            return true;
        }

        public int GetCost(WallType wall, int ring)
        {
            if (wall == WallType.Produce)
                return WallsManager.Instance.WallsProgress.ProduceWallCostDefault + WallsManager.Instance.WallsProgress.ProduceWallCostIncrease * ring;
            if (wall == WallType.Attack)
                return WallsManager.Instance.WallsProgress.AttackWallCostDefault + WallsManager.Instance.WallsProgress.AttackWallCostIncrease * ring;
            if (wall == WallType.Protect)
                return WallsManager.Instance.WallsProgress.ProtectWallCostDefault + WallsManager.Instance.WallsProgress.ProtectWallCostIncrease * ring;

            return 0;
        }
        #endregion
    }
}
