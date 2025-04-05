using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class Movement : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 10)] private float _speed = 5f;
        [SerializeField] private InputActionReference _inputAction;
        #endregion

        #region UNITY CALLBACKS
        private void Update()
        {
            Move(_inputAction.action.ReadValue<Vector2>());
        }
        #endregion

        #region METHODS PRIVATE
        private void Move(Vector2 direction)
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        #endregion
    }
}
