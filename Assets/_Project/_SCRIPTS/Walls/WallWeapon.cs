using UnityEngine;

namespace Gameplay
{
    public class WallWeapon : MonoBehaviour
    {
        [SerializeField] private WallWeaponProjectile _weaponProjectilePrefab;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _weaponRange;
        [SerializeField] private float _damage;
        [SerializeField] private float _cooldown;

        private float _cooldownTimer;

        public void Shoot(WallDirection direction)
        {
            if (_cooldownTimer >= Time.time) return;
            Transform enemy = GetEnemy(direction);

            if (enemy == null) return;

            _cooldownTimer = Time.time + _cooldown;
            var projectile = Instantiate(_weaponProjectilePrefab, _firePoint.position, Quaternion.identity);
            projectile.Init(_damage, (enemy.position - _firePoint.position).normalized);
        }

        private Transform GetEnemy(WallDirection direction)
        {
            Vector3 vectorDirection = WallsSettings.Instance.GetDirectionVector(direction);
            Vector3 angle = WallsSettings.Instance.GetWallAngle(direction);
            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position + vectorDirection.normalized * _weaponRange / 2, Vector2.one * _weaponRange, angle.z, vectorDirection, _weaponRange);
            Transform enemy = null;
            float closestDistance = 1000000;

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.tag == "EnemyFly")
                {
                    float distance = Vector2.Distance(transform.position, hit.point);
                    if ((enemy != null && enemy.tag == "EnemyGround") || distance < closestDistance)
                    {
                        closestDistance = distance;
                        enemy = hit.transform;
                    }
                }
                if (hit.transform.tag == "EnemyGround")
                {
                    if (enemy != null && enemy.tag == "EnemyFly")
                        continue;
                    else
                    {
                        float distance = Vector2.Distance(transform.position, hit.point);
                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            enemy = hit.transform;
                        }
                    }
                }
            }

            return enemy;
        }
    }
}