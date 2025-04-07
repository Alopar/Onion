namespace Gameplay
{
    public class WallPreview : Wall
    {
        private void OnMouseEnter()
        {

        }

        private void OnMouseOver()
        {

        }

        private void OnMouseExit()
        {

        }

        private void OnMouseDown()
        {
            WallType wallToCreate = WallCreator.Instance.SelectedWall;

            int cost = 0;
            if (wallToCreate == WallType.Produce)
                cost = WallsManager.Instance.WallsProgress.ProduceWallCostDefault + WallsManager.Instance.WallsProgress.ProduceWallCostIncrease * _wallRing;
            if (wallToCreate == WallType.Attack)
                cost = WallsManager.Instance.WallsProgress.AttackWallCostDefault + WallsManager.Instance.WallsProgress.AttackWallCostIncrease * _wallRing;
            if (wallToCreate == WallType.Protect)
                cost = WallsManager.Instance.WallsProgress.ProtectWallCostDefault + WallsManager.Instance.WallsProgress.ProtectWallCostIncrease * _wallRing;

            if (!ResourcesManager.Instance.TryDecreaseEnergy(cost)) return;

            if (wallToCreate == WallType.Attack)
                WallsManager.Instance.CreateWall<AttackWall>(WallDirection);
            if (wallToCreate == WallType.Protect)
                WallsManager.Instance.CreateWall<ProtectWall>(WallDirection);
            if (wallToCreate == WallType.Produce)
                WallsManager.Instance.CreateWall<ProduceWall>(WallDirection);
        }

        protected override void Destroy() =>
            WallsManager.Instance.DestroyWall(this);
    }
}