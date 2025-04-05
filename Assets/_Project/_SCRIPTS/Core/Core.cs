using UnityEngine;

namespace Gameplay
{
    public class Core : MonoBehaviour
    {
        #region METHODS PUBLIC
        public void Destroy()
        {
            print("💥💥💥 CORE DESTROYED, GAME OVER! 💥💥💥");
            Destroy(gameObject);
        }
        #endregion
    }
}
