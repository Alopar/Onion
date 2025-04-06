using NaughtyAttributes;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class EnergyResourceView : MonoBehaviour
    {
        [SerializeField, Required] private TextMeshProUGUI _text;

        private bool _initialized;

        private void Start()
        {
            _initialized = true;
            ResourcesManager.Instance.OnEnergyChanged += UpdateView;
            UpdateView(ResourcesManager.Instance.Energy);
        }

        private void OnEnable()
        {
            if (!_initialized) return;

            ResourcesManager.Instance.OnEnergyChanged += UpdateView;
            UpdateView(ResourcesManager.Instance.Energy);
        }

        private void OnDisable() =>
            ResourcesManager.Instance.OnEnergyChanged -= UpdateView;

        private void UpdateView(int energy) =>
            _text.text = energy.ToString();
    }
}