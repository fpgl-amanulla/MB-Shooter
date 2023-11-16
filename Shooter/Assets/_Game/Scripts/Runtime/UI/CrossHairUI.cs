using System;
using _Game.Managers;
using _Game.Scripts.Runtime.Ammo;
using _Game.Scripts.Runtime.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI
{
    public class CrossHairUI : MonoBehaviour
    {
        [SerializeField] private Image _crossHair;
        [SerializeField] private RectTransform _canvas;

        [SerializeField] private Transform _firePointTr;
        [SerializeField] private LayerMask _layerMask;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            _crossHair.gameObject.SetActive(false);
            GameManager.Instance.OnLevelStart += () => { _crossHair.gameObject.SetActive(true); };
        }

        private void LateUpdate()
        {
            if (GameManager.Instance.IsGameOver)
            {
                _crossHair.gameObject.SetActive(false);
                return;
            }

            UpdateCrossHair();
        }

        private void UpdateCrossHair()
        {
            Vector3 gunTipPoint = _firePointTr.position;
            Vector3 gunForward = _firePointTr.forward;
            Vector3 hitPoint = gunTipPoint + gunForward * 10;

            if (Physics.Raycast(gunTipPoint, gunForward, out RaycastHit hit, float.MaxValue, _layerMask))
            {
                hitPoint = hit.point;
            }

            Vector3 screenSpaceLocation = _camera.WorldToScreenPoint(hitPoint);

            Vector2 position = _crossHair.rectTransform.anchoredPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, screenSpaceLocation, null, out Vector2 localPoint))
            {
                _crossHair.rectTransform.anchoredPosition = Vector2.Lerp(position, localPoint, 0.05f);
            }
            else
            {
                _crossHair.rectTransform.anchoredPosition = Vector2.Lerp(position, Vector2.zero, 0.05f);
            }
        }
    }
}