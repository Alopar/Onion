using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public abstract class Wall : MonoBehaviour
    {
        [SerializeField, Required] protected EdgeCollider2D _edgeCollider;
        [SerializeField, Required] protected LineRenderer _lineRenderer;
        [SerializeField] protected BuildingHealth _health;
        [SerializeField] protected GameObject _tower1;
        [SerializeField] protected GameObject _tower2;
        [SerializeField] protected float _explodeRange;

        protected WallDirection _wallDirection;
        protected int _wallRing;

        public WallDirection WallDirection => _wallDirection;

        public virtual void Init(WallDirection direction, int ring)
        {
            _wallDirection = direction;
            _wallRing = ring;
        }

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

            if (_tower1 == null || _tower2 == null) return;

            float towerAngle = 0;
            float towerX = Mathf.Cos(towerAngle) * radius;
            float towerY = Mathf.Sin(towerAngle) * radius;
            _tower1.transform.localPosition = new Vector3(towerX, towerY, 0);

            towerAngle = angle * Mathf.PI;
            towerX = Mathf.Cos(towerAngle) * radius;
            towerY = Mathf.Sin(towerAngle) * radius;
            _tower2.transform.localPosition = new Vector3(towerX, towerY, 0);
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

        private void HealthChanged(float current, float max)
        {
            // TODO: change broken state
        }

        protected virtual void Destroy()
        {
            Vector3 vectorDirection = WallsSettings.Instance.GetDirectionVector(_wallDirection);
            Vector3 angle = WallsSettings.Instance.GetWallAngle(_wallDirection);
            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position + vectorDirection.normalized * _explodeRange / 2, Vector2.one * _explodeRange, angle.z, vectorDirection, _explodeRange, 3 << 3);

            float totalDamage = GetExplosionDamage();
            float dmg = totalDamage / hits.Length;

            foreach (RaycastHit2D hit in hits)
                hit.transform.GetComponent<EnemyHealth>().DealDamage(dmg);

            WallsManager.Instance.DestroyWall(this);
        }

        protected virtual float GetExplosionDamage() => 0;

        private void OnEnable()
        {
            if (_health == null) return;

            _health.OnHealthChanged += HealthChanged;
            _health.OnDeath += Destroy;
        }

        private void OnDisable()
        {
            if (_health == null) return;

            _health.OnHealthChanged -= HealthChanged;
            _health.OnDeath -= Destroy;
        }
    }
}
