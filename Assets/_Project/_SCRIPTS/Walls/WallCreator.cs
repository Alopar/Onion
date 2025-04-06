using System;
using UnityEngine;

namespace Gameplay
{
    public class WallCreator : MonoBehaviour
    {
        private WallType _selectedWall;

        public WallType SelectedWall => _selectedWall;

        public event Action<WallType> WallSelectionChanged;
        
        public static WallCreator Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }

            Instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _selectedWall = WallType.Produce;
                WallSelectionChanged?.Invoke(_selectedWall);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                _selectedWall = WallType.Attack;
                WallSelectionChanged?.Invoke(_selectedWall);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                _selectedWall = WallType.Protect;
                WallSelectionChanged?.Invoke(_selectedWall);
            }
        }
    }
}