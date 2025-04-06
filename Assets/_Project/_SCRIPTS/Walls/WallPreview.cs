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
                cost = WallsManager.Instance.WallsProgress.Costs[0];
            if (wallToCreate == WallType.Attack)
                cost = WallsManager.Instance.WallsProgress.Costs[1];
            if (wallToCreate == WallType.Protect)
                cost = WallsManager.Instance.WallsProgress.Costs[2];

            if (!ResourcesManager.Instance.TryDecreaseEnergy(cost)) return;

            if (wallToCreate == WallType.Attack)
                WallsManager.Instance.CreateWall<AttackWall>(WallDirection);
            if (wallToCreate == WallType.Protect)
                WallsManager.Instance.CreateWall<ProtectWall>(WallDirection);
            if (wallToCreate == WallType.Produce)
                WallsManager.Instance.CreateWall<ProduceWall>(WallDirection);
        }
    }
}