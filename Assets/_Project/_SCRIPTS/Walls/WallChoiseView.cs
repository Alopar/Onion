using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class WallChoiseView : MonoBehaviour
    {
        [SerializeField, Required] private Image _produceWallImage;
        [SerializeField, Required] private TextMeshProUGUI _produceCost;
        [SerializeField, Required] private Image _attackWallImage;
        [SerializeField, Required] private TextMeshProUGUI _attackCost;
        [SerializeField, Required] private Image _protectWallImage;
        [SerializeField, Required] private TextMeshProUGUI _protectCost;

        [SerializeField] private float _selectedSize;

        public static WallChoiseView Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            WallCreator.Instance.WallSelectionChanged += SelectWall;
            SelectWall(WallCreator.Instance.SelectedWall);
        }

        public void HideCosts()
        {
            _produceCost.gameObject.SetActive(false);
            _attackCost.gameObject.SetActive(false);
            _protectCost.gameObject.SetActive(false);
        }

        public void ShowCosts(int ring)
        {
            _produceCost.gameObject.SetActive(true);
            _attackCost.gameObject.SetActive(true);
            _protectCost.gameObject.SetActive(true);

            _produceCost.text = ResourcesManager.Instance.GetCost(WallType.Produce, ring).ToString();
            _attackCost.text = ResourcesManager.Instance.GetCost(WallType.Attack, ring).ToString();
            _protectCost.text = ResourcesManager.Instance.GetCost(WallType.Protect, ring).ToString();
        }

        private void SelectWall(WallType wallType)
        {
            _produceWallImage.transform.localScale = Vector3.one * (wallType == WallType.Produce ? _selectedSize : 1);
            _protectWallImage.transform.localScale = Vector3.one * (wallType == WallType.Protect ? _selectedSize : 1);
            _attackWallImage.transform.localScale = Vector3.one * (wallType == WallType.Attack ? _selectedSize : 1);
        }
    }
}