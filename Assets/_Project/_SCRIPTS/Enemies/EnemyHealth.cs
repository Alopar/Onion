using System;
using DG.Tweening;
using UnityEngine;

namespace Gameplay
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        #region FIELDS INSPECTOR
        [SerializeField] private float _healthMax;
        
        [Space(10)]
        [Header("✨ Damage Feedback ✨")]
        [SerializeField, Range(0, 1)] private float _duration;
        [SerializeField, Range(0, 2)] private float _scaleMagnitude;
        
        [Space(10)]
        [SerializeField] private Transform _view;
        [SerializeField] private SpriteRenderer _mask;
        #endregion

        #region FIELDS PRIVATE
        private float _healthCurrent;
        #endregion

        #region EVENTS
        public event Action<float, float> OnHealthChanged;
        public event Action OnDeath;
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            _healthCurrent = _healthMax;
        }

        private void OnDestroy()
        {
            _view.DOKill();
            _mask.DOKill();
        }
        #endregion

        #region METHODS PUBLIC
        public void DealDamage(float damage)
        {
            if (_healthCurrent <= 0) return;

            _healthCurrent -= damage;
            OnHealthChanged?.Invoke(_healthCurrent, _healthMax);

            if (_healthCurrent > 0)
            {
                ShowDamageFeedback();
                return;
            }

            ShowDeathFeedback();
            OnDeath?.Invoke();
        }
        #endregion

        #region METHODS PRIVATE
        private void ShowDamageFeedback()
        {
            _view.DOKill();
            _mask.DOKill();
            _view.DOScale(Vector3.one * _scaleMagnitude, _duration).SetLoops(2, LoopType.Yoyo);
            _mask.DOFade(1f, _duration).From(0f).SetLoops(2, LoopType.Yoyo);
        }

        private void ShowDeathFeedback()
        {
            _mask.DOKill();
            _view.DOKill();
            _mask.DOFade(1f, _duration).From(0f).SetLoops(2, LoopType.Yoyo);
            _view.DOScale(Vector3.one * _scaleMagnitude, _duration).SetLoops(2, LoopType.Yoyo).OnComplete(() => {
                _view.DOScale(Vector3.zero, _duration);
                Destroy(gameObject, _duration);
            });
        }
        #endregion
    }
}
