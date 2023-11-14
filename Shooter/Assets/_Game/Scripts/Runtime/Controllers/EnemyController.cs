using System;
using System.Collections;
using _Game.Scripts.Runtime.Interface;
using _Tools.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace _Game.Scripts.Runtime.Controllers
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        [SerializeField] private GameObject _gunObj;

        [FoldoutGroup("HealthBar")] [SerializeField] private float _maxHealth = 100;
        [FoldoutGroup("HealthBar")] [SerializeField] private Image _fillImage;
        [FoldoutGroup("HealthBar")] [SerializeField] private GameObject _healthBar;

        private float _currentHealth;
        private bool _isDied;
        private Color _heathBarColor;

        private static readonly int Die = Animator.StringToHash("Die");

        private void Start()
        {
            _currentHealth = _maxHealth;
            _heathBarColor = _fillImage.color;
            UpdateHealthBar();
        }

        public void TakeDamage(float amount)
        {
            if (_isDied) return;

            _currentHealth -= amount;
            UpdateHealthBar();
            
            _skinnedMeshRenderer.MaterialSplashEffect(Color.white, Color.red, .08f);
            StartCoroutine(HealthFlash());
            
            if (_currentHealth <= 0)
            {
                _isDied = true;
                _animator.SetTrigger(Die);
                _healthBar.SetActive(false);
                DropGun();
            }
        }

        private void UpdateHealthBar()
        {
            _fillImage.fillAmount = _currentHealth / _maxHealth;
        }

        private IEnumerator HealthFlash()
        {
            _fillImage.color = Color.white;
            yield return new WaitForSeconds(.1f);
            _fillImage.color = _heathBarColor;
        }

        private void DropGun()
        {
            _gunObj.transform.parent = null;
            if (_gunObj.GetComponent<Collider>())
                _gunObj.GetComponent<Collider>().enabled = true;

            if (!_gunObj.GetComponent<Rigidbody>()) _gunObj.AddComponent<Rigidbody>();

            Destroy(_gunObj, 3.0f);
        }
    }
}