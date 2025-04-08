using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private TextMeshProUGUI _time;

        private void Update()
        {
            int remainingSeconds = (int)(_gameManager.GameTimeMax - _gameManager.GameTimeCurrent);
            int sec = remainingSeconds % 60;
            int min = remainingSeconds / 60;
            string secString = sec < 10 ? $"0{sec}" : sec.ToString();
            _time.text = $"{min}:{secString}";
        }
    }
}
