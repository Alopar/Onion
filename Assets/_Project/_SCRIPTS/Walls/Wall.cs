using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public abstract class Wall : MonoBehaviour
    {
        [SerializeField] protected EdgeCollider2D _edgeCollider;
        [SerializeField] protected LineRenderer _lineRenderer;

        protected WallDirection _wallDirection;
        public WallDirection WallDirection => _wallDirection;

        public void SetDirection(WallDirection direction) =>
            _wallDirection = direction;

        public void Draw(int segmentsCount, float angle, float radius)
        {
            _lineRenderer.positionCount = segmentsCount;
            angle = angle / 180;
            for (int i = 0; i < segmentsCount; i++)
            {
                float localAngle = angle * Mathf.PI * (i + 0.5f) / segmentsCount;
                float x = Mathf.Cos(localAngle) * radius;
                float y = Mathf.Sin(localAngle) * radius;
                _lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
            }
        }

        public void UpdateCollider(int segmentsCount)
        {
            List<Vector2> points = new(segmentsCount);
            int lineLength = _lineRenderer.positionCount;
            float step = (lineLength - 1) / (segmentsCount - 1);
            for (int i = 0; i <= segmentsCount; i++)
                points.Add(_lineRenderer.GetPosition(Mathf.Clamp(Mathf.CeilToInt(i * step), 0, lineLength - 1)));

            _edgeCollider.SetPoints(points);
        }
    }
}
