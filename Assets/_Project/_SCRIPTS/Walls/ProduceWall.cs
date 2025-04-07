using UnityEngine;

namespace Gameplay
{
    public class ProduceWall : Wall
    {
        [SerializeField] private EnergyMiner _energyMiner;

        public override void Init(WallDirection direction, int ring)
        {
            base.Init(direction, ring);

            _health.SetMaxHealth(WallsManager.Instance.WallsProgress.ProduceWallDefaultHealth + _wallRing * WallsManager.Instance.WallsProgress.ProduceWallHealthIncrease);
            _energyMiner.SetIncome(WallsManager.Instance.WallsProgress.ProduceWallIncome + _wallRing * WallsManager.Instance.WallsProgress.ProduceWallIncomeIncrease);
            _energyMiner.SetCooldown(WallsManager.Instance.WallsProgress.ProduceWallIncomeCooldown + _wallRing * WallsManager.Instance.WallsProgress.ProduceWallIncomeCooldownIncrease);
        }

        protected override float GetExplosionDamage() =>
            _health.MaxHealth * WallsManager.Instance.WallsProgress.ProduceWallDestroyDamageMultiplier;
    }
}
