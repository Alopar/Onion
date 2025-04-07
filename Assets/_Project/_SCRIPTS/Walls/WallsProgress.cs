using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "Walls Progress")]
    public class WallsProgress : ScriptableObject
    {
        public int ProtectWallDefaultHealth;
        public int ProtectWallHealthIncrease;
        public float ProtectWallDefaultReflectDamage;
        public float ProtectWallReflectDamageDecrease;
        public float ProtectWallReflectDamageMin;
        public int ProtectWallCostDefault;
        public int ProtectWallCostIncrease;
        public float ProtectWallDestroyDamageMultiplier;

        public int AttackWallDefaultHealth;
        public int AttackWallHealthIncrease;
        public int AttackWallsWeaponsDefault;
        public int AttackWallsWeaponsIncrease;
        public float AttackWallsCooldownDefault;
        public float AttackWallsCooldownIncrease;
        public float AttackWallsDamageDefault;
        public int AttackWallCostDefault;
        public int AttackWallCostIncrease;
        public float AttackWallDestroyDamageMultiplier;

        public int ProduceWallDefaultHealth;
        public int ProduceWallHealthIncrease;
        public int ProduceWallIncome;
        public int ProduceWallIncomeIncrease;
        public float ProduceWallIncomeCooldown;
        public float ProduceWallIncomeCooldownIncrease;
        public int ProduceWallCostDefault;
        public int ProduceWallCostIncrease;
        public float ProduceWallDestroyDamageMultiplier;
    }
}
