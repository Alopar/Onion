using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerMovement : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 10)] private float _speed = 5f;
        [SerializeField] private InputActionReference _inputAction;
        #endregion

        #region UNITY CALLBACKS
        private void Update()
        {
            var input = _inputAction.action.ReadValue<Vector2>();
            var direction = new Vector2(input.x, input.y);
            Move(direction);
        }
        #endregion

        #region METHODS PRIVATE
        private void Move(Vector3 direction)
        {
            transform.Translate(direction.normalized * (_speed * Time.deltaTime));
        }
        #endregion
    }
}
