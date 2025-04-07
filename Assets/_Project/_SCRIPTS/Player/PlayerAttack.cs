using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

namespace Gameplay
{
    public class PlayerAttack : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 999)] private float _damage;
        [SerializeField, Range(0, 99)] private float _cooldown;

        [Space(10)]
        [SerializeField] private Transform _firePoint;
        [SerializeField] private PlayerProjectile _projectilePrefab;

        [Space(10)]
        [SerializeField] private InputActionReference _fireInputAction;
        [SerializeField] private InputActionReference _lookInputAction;

        [Space(10)]
        [Header("✨ Attack Feedback ✨")]
        [SerializeField, Range(0, 1)] private float _duration;
        [SerializeField] private Light2D _light;
        [SerializeField] private List<AudioClip> _soundEffects;
        #endregion

        #region FIELDS PRIVATE
        private Camera _camera;
        private float _cooldownTimer;
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Time.timeScale == 0) return;
            if (!_fireInputAction.action.IsPressed()) return;

            Attack();
        }
        #endregion

        #region METHODS PRIVATE
        private void Attack()
        {
            if (_cooldownTimer >= Time.time) return;

            _cooldownTimer = Time.time + _cooldown;
            var projectile = Instantiate(_projectilePrefab, _firePoint.position, Quaternion.identity);
            Rotate(projectile.transform, GetLookDirection());
            projectile.Init(_damage);

            ShowDamageFeedback();
        }

        private void Rotate(Transform projectile, Vector3 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        private Vector2 GetLookDirection()
        {
            var worldPoint = _camera.WorldToScreenPoint(_firePoint.position);
            var screenPosition = new Vector2(worldPoint.x, worldPoint.y);
            var mousePosition = _lookInputAction.action.ReadValue<Vector2>();

            return (mousePosition - screenPosition).normalized;;
        }

        private void ShowDamageFeedback()
        {
            var clip = _soundEffects[Random.Range(0, _soundEffects.Count)];
            SoundManager.Instance.PlaySound(clip);
            DOVirtual.Float(2f, 3f, _duration, (value) => { _light.intensity = value; }).SetLoops(2, LoopType.Yoyo);
            DOVirtual.Float(3f, 3.5f, _duration, (value) => { _light.pointLightOuterRadius = value; }).SetLoops(2, LoopType.Yoyo);
        }
        #endregion
    }
}
