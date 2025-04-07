using UnityEngine;

namespace Gameplay
{
    public class ProtectWall : Wall
    {
        public override void Init(WallDirection direction, int ring)
        {
            base.Init(direction, ring);

            _health.SetMaxHealth(WallsManager.Instance.WallsProgress.ProtectWallDefaultHealth + _wallRing * WallsManager.Instance.WallsProgress.ProtectWallHealthIncrease);
        }

        public float GetReflectDamage() =>
            Mathf.Clamp(
                WallsManager.Instance.WallsProgress.ProtectWallDefaultReflectDamage 
                - WallsManager.Instance.WallsProgress.ProtectWallReflectDamageDecrease * _wallRing,
                WallsManager.Instance.WallsProgress.ProtectWallReflectDamageMin, 100);

        protected override float GetExplosionDamage() =>
            _health.MaxHealth * WallsManager.Instance.WallsProgress.ProtectWallDestroyDamageMultiplier;
    }
}