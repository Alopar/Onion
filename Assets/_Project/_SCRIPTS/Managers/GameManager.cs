using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class GameManager : MonoBehaviour
    {
        #region METHODS PUBLIC
        [Button("⭐ START GAME ⭐")]
        public async void StartGame()
        {
            await Awaitable.WaitForSecondsAsync(0.25f);
            SceneManager.LoadScene(1);
        }
        #endregion
    }
}
