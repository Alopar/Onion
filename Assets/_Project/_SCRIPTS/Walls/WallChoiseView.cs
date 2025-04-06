using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class WallChoiseView : MonoBehaviour
    {
        [SerializeField, Required] private Image _produceWallSelectedImage;
        [SerializeField, Required] private Image _attackWallSelectedImage;
        [SerializeField, Required] private Image _protectWallSelectedImage;

        private void Start()
        {
            WallCreator.Instance.WallSelectionChanged += SelectWall;
            SelectWall(WallCreator.Instance.SelectedWall);
        }

        private void SelectWall(WallType wallType)
        {
            _produceWallSelectedImage.gameObject.SetActive(wallType == WallType.Produce);
            _protectWallSelectedImage.gameObject.SetActive(wallType == WallType.Protect);
            _attackWallSelectedImage.gameObject.SetActive(wallType == WallType.Attack);
        }
    }
}