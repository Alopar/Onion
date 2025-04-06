using UnityEngine;

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
            int rand = Random.Range(0, 3);
            if (rand == 0)
                WallsManager.Instance.CreateWall<AttackWall>(WallDirection);
            if (rand == 1)
                WallsManager.Instance.CreateWall<ProtectWall>(WallDirection);
            if (rand == 2)
                WallsManager.Instance.CreateWall<ProduceWall>(WallDirection);
        }
    }
}
