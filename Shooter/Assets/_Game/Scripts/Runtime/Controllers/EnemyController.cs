using System;
using System.Collections;
using _Game.Managers;
using _Game.Scripts.Runtime.DataManager;
using _Game.Scripts.Runtime.Interface;
using _Tools.Extensions;
using Lean.Pool;
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

        [FoldoutGroup("Attributes")] [SerializeField] private int _damagePower = 10;
        [FoldoutGroup("Attributes")] [SerializeField] private float _rotationSmoothness = 1200;
        [FoldoutGroup("Attributes")] [SerializeField] private float _movementSpeed = 10;
        [FoldoutGroup("Attributes")] [SerializeField] private float _stoppingDistance = 2;

        [FoldoutGroup("HealthBar")] [SerializeField] private float _maxHealth = 100;
        [FoldoutGroup("HealthBar")] [SerializeField] private Image _fillImage;
        [FoldoutGroup("HealthBar")] [SerializeField] private GameObject _healthBar;

        private float _currentHealth;
        private bool _isDied;
        private Color _heathBarColor;
        private bool _canMove;
        private Transform _playerTr;

        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        private void Start()
        {
            _canMove = true;
            _currentHealth = _maxHealth;
            _heathBarColor = _fillImage.color;
            UpdateHealthBar();

            _playerTr = PlayerController.Instance.GetPlayerTr();
        }

        private void Update()
        {
            if (_canMove)
            {
                _animator.SetBool(IsRunning, true);
                Vector3 direction = _playerTr.position - transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(direction.Flat(), Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, _rotationSmoothness * Time.deltaTime);

                transform.Translate(Vector3.forward * (_movementSpeed * Time.deltaTime));

                if (GetStoppingDistance() < _stoppingDistance)
                {
                    _canMove = false;
                    PlayerController.Instance.TakeDamage(_damagePower);
                    Destroy(gameObject);
                }
            }
        }

        private float GetStoppingDistance() => Vector3.Distance(transform.position, _playerTr.position);

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
                _canMove = false;
                _animator.SetTrigger(Die);
                _healthBar.SetActive(false);

                GameDataManager.Instance.UpdateKillCount();

                DropGun();
                Destroy(gameObject, 2.0f);
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