using UnityEngine;

namespace Gameplay
{
    public class WallWeaponProjectile : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private float _damage;
        private Vector3 _attackDirection;

        private void OnCollisionEnter2D(Collision2D other)
        {
            var enemyHealth = other.gameObject.GetComponentInParent<EnemyHealth>();
            if (enemyHealth == null) return;

            enemyHealth.DealDamage(_damage);
            Destroy(gameObject);
        }

        private void Update() =>
            transform.position += _attackDirection * (_speed * Time.deltaTime);


        public void Init(float damage, Vector2 attackDirection)
        {
            _damage = damage;
            _attackDirection = attackDirection;
            Destroy(gameObject, 3f);
        }
    }
}