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

            if (wallToCreate == WallType.Attack)
                WallsManager.Instance.CreateWall<AttackWall>(WallDirection);
            if (wallToCreate == WallType.Protect)
                WallsManager.Instance.CreateWall<ProtectWall>(WallDirection);
            if (wallToCreate == WallType.Produce)
                WallsManager.Instance.CreateWall<ProduceWall>(WallDirection);
        }
    }
}