using UnityEngine;

namespace Gameplay
{
    public class WallPreview : Wall
    {
        [SerializeField] private Material _defaultMaterial;
        [SerializeField] private Material _protectMaterial;
        [SerializeField] private Material _attackWallMaterial;
        [SerializeField] private Material _produceWallMaterial;

        private void OnMouseEnter()
        {
            _lineRenderer.startColor = new Color(1, 1, 1, 0.2f);
            _lineRenderer.endColor = new Color(1, 1, 1, 0.2f);
        }

        private void OnMouseOver()
        {
            WallType selectedWall = WallCreator.Instance.SelectedWall;

            if (selectedWall == WallType.Produce)
                _lineRenderer.sharedMaterial = _produceWallMaterial;
            if (selectedWall == WallType.Attack)
                _lineRenderer.sharedMaterial = _attackWallMaterial;
            if (selectedWall == WallType.Protect)
                _lineRenderer.sharedMaterial = _protectMaterial;

            if (ResourcesManager.Instance.Energy <= ResourcesManager.Instance.GetCost(selectedWall, _wallRing))
            {
                _lineRenderer.startColor = new Color(0.8f, 0, 0, 0.7f);
                _lineRenderer.endColor = new Color(0.8f, 0, 0, 0.7f);
            }
            else
            {
                _lineRenderer.startColor = new Color(1, 1, 1, 0.2f);
                _lineRenderer.endColor = new Color(1, 1, 1, 0.2f);
            }
        }

        private void OnMouseExit()
        {
            _lineRenderer.sharedMaterial = _defaultMaterial;

            _lineRenderer.startColor = new Color(1, 1, 1, 0.4f);
            _lineRenderer.endColor = new Color(1, 1, 1, 0.4f);
        }

        private void OnMouseDown()
        {
            WallType wallToCreate = WallCreator.Instance.SelectedWall;

            int cost = ResourcesManager.Instance.GetCost(wallToCreate, _wallRing);

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