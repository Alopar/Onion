using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private bool _useTimer;
        [SerializeField, ShowIf("_useTimer")] private float _gameTime = 600f;
        [SerializeField, ShowIf("_useTimer")] private UnityEvent _onTimerEnd;
        #endregion

        #region FIELDS PRIVATE
        private float _gameTimeCurrent;
        #endregion

        #region PROPERTIES
        public float GameTimeMax => _gameTime;
        public float GameTimeCurrent => _gameTimeCurrent;
        #endregion
        
        #region METHODS PUBLIC
        [Button("⭐ START GAME ⭐")]
        public void StartGame()
        {
            StartCoroutine(DelayCall(0.25f, () => { SceneManager.LoadScene(1); }));
        }

        [Button("💢 RESTART GAME 💢")]
        public void RestartGame()
        {
            StartCoroutine(DelayCall(0.25f, () => { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }));
        }

        [Button("🎲 START MENU 🎲")]
        public void StartMenu()
        {
            StartCoroutine(DelayCall(0.25f, () => { SceneManager.LoadScene(0); }));
        }

        [Button("💤 PAUSE GAME 💤")]
        public void PauseGame()
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            Time.timeScale = 1.0f;
        }

        private void FixedUpdate()
        {
            if (!_useTimer) return;

            _gameTimeCurrent += Time.deltaTime;
            if (_gameTimeCurrent < _gameTime) return;

            PauseGame();
            _onTimerEnd.Invoke();
            print("🏆🏆🏆 TIME IS OVER, YOU ARE WIN! 🏆🏆🏆");
        }
        #endregion

        #region COROUTINES
        private IEnumerator DelayCall(float seconds, Action callback)
        {
            yield return new WaitForSecondsRealtime(seconds);
            callback?.Invoke();
        }
        #endregion
    }
}
