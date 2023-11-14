using System;
using System.Collections;
using _Game.Managers;
using _Tools.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.UI
{
    public class PlayerHealthUI : Singleton<PlayerHealthUI>
    {
        [SerializeField] private GameObject _visual;
        [SerializeField] private Image _fillImage;

        private Color _heathBarColor;

        private void Start()
        {
            _visual.SetActive(false);
            GameManager.Instance.OnLevelStart += OnLevelStart;
            GameManager.Instance.OnLevelFail += OnLevelFail;

            _heathBarColor = _fillImage.color;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnLevelStart -= OnLevelStart;
            GameManager.Instance.OnLevelFail -= OnLevelFail;
        }

        public void TakeDamage(float amount)
        {
            _fillImage.fillAmount = amount;
            StartCoroutine(HealthFlash());
        }

        private IEnumerator HealthFlash()
        {
            _fillImage.color = Color.white;
            yield return new WaitForSeconds(.1f);
            _fillImage.color = _heathBarColor;
        }

        private void OnLevelFail() => _visual.SetActive(false);

        private void OnLevelStart() => _visual.SetActive(true);
    }
}