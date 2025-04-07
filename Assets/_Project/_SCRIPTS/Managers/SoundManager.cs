using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class SoundManager : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundSource;
        
        [Space(10)]
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private Slider _soundSlider;
        #endregion

        #region FIELDS PRIVATE
        private static SoundManager _instance;
        #endregion

        #region PROPERTIES
        public static SoundManager Instance => _instance;
        #endregion

        #region UNITY CALLBACKS
        private void Awake()
        {
            _instance = this;
        }

        private void OnEnable()
        {
            _musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            _soundSlider.onValueChanged.AddListener(OnSoundVolumeChanged);
        }

        private void OnDisable()
        {
            _musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
            _soundSlider.onValueChanged.RemoveListener(OnSoundVolumeChanged);
        }

        private void Start()
        {
            _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            _soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 0.75f);
        }
        #endregion

        #region METHODS PRIVATE
        private void OnMusicVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
            _musicSource.volume = value;
        }

        private void OnSoundVolumeChanged(float value)
        {
            PlayerPrefs.SetFloat("SoundVolume", value);
            _soundSource.volume = value;
        }
        #endregion

        #region METHODS PUBLIC
        public void PlaySound(AudioClip clip)
        {
            _soundSource.PlayOneShot(clip);
        }
        #endregion
    }
}
