using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class CameraOffset : MonoBehaviour
    {
        #region FIELDS INSPECTOR
        [SerializeField, Range(0, 5)] private float _offsetDelta = 3f;
        [SerializeField, Range(0, 1)] private float _verticalAspect = 1f;
        [SerializeField, Range(0, 1)] private float _horizontalAspect = 0.25f;
        [SerializeField, Range(0, 5)] private float _offsetSpeed = 1f;

        [Space(10)]
        [SerializeField] private InputActionReference _inputAction;
        [SerializeField] private CinemachineCameraOffset _cinemachineCameraOffset;
        #endregion

        #region FIELDS PRIVATE
        private Camera _camera;
        #endregion

        #region UNITY CALLBACKS
        private void Start()
        {
            _camera = Camera.main;
        }

        private void LateUpdate()
        {
            var offset = GetDirection() * _offsetDelta;
            offset.y *= _verticalAspect;
            offset.x *= _horizontalAspect;
            _cinemachineCameraOffset.Offset = Vector2.Lerp(_cinemachineCameraOffset.Offset, offset, _offsetSpeed * Time.deltaTime);
        }
        #endregion

        #region METHODS PRIVATE
        private Vector2 GetDirection()
        {
            var cameraAtScreen = _camera.WorldToScreenPoint(transform.position);
            var cameraPosition = new Vector2(cameraAtScreen.x, cameraAtScreen.y);
            var mousePosition = _inputAction.action.ReadValue<Vector2>();

            return (mousePosition - cameraPosition).normalized;
        }
        #endregion
    }
}
