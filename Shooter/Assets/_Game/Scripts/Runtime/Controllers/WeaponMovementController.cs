using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Runtime.Controllers
{
    public class WeaponMovementController : MonoBehaviour
    {
        public float rotationSpeed = 5f;

        [BoxGroup("HorizontalGroup")] public float minHorizontalRotation = -45f;
        [BoxGroup("HorizontalGroup")] public float maxHorizontalRotation = 45f;

        [BoxGroup("VerticalGroup")] public float minVerticalRotation = -45f;
        [BoxGroup("VerticalGroup")] public float maxVerticalRotation = 45f;

        private bool _isDragging;
        private Vector3 _dragStartPosition;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) StartDragging();
            else if (Input.GetMouseButtonUp(0)) StopDragging();

            if (_isDragging) RotateObject();
        }

        private void StartDragging()
        {
            _isDragging = true;
            _dragStartPosition = Input.mousePosition;
        }

        private void StopDragging() => _isDragging = false;

        private void RotateObject()
        {
            float deltaX = Input.mousePosition.x - _dragStartPosition.x;
            float deltaY = Input.mousePosition.y - _dragStartPosition.y;

            float horizontalRotationAngle = deltaX * rotationSpeed * Time.deltaTime;
            float verticalRotationAngle = -deltaY * rotationSpeed * Time.deltaTime;

            Vector3 currentRotation = transform.localEulerAngles;

            // Horizontal rotation
            float newHorizontalRotation = currentRotation.y + horizontalRotationAngle;
            newHorizontalRotation = ClampRotation(newHorizontalRotation, minHorizontalRotation, maxHorizontalRotation);

            // Vertical rotation
            float newVerticalRotation = currentRotation.x + verticalRotationAngle;
            newVerticalRotation = ClampRotation(newVerticalRotation, minVerticalRotation, maxVerticalRotation);

            transform.localEulerAngles = new Vector3(newVerticalRotation, newHorizontalRotation, currentRotation.z);

            _dragStartPosition = Input.mousePosition;
        }

        private float ClampRotation(float angle, float min, float max)
        {
            if (angle > 180f) angle -= 360f;

            return Mathf.Clamp(angle, min, max);
        }
    }
}