using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "Walls Progress")]
    public class WallsProgress : ScriptableObject
    {
        public List<int> AttackWallsWeapons;
        public List<int> Costs;

        public int GetWallsCount(int index) =>
            index >= AttackWallsWeapons.Count ? AttackWallsWeapons[^1] : AttackWallsWeapons[index];
    }
}
