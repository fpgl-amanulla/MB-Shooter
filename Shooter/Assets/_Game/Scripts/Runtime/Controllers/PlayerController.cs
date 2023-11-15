using System;
using _Game.Managers;
using _Game.Scripts.Runtime.Interface;
using _Game.UI;
using _Tools.Helpers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Game.Scripts.Runtime.Controllers
{
    public class PlayerController : Singleton<PlayerController>, IDamageable
    {
        [SerializeField] private float _maxHealth;

        private WeaponMovementController _weaponMovementController;
        private WeaponShootingController _weaponShootingController;

        [ReadOnly, ShowInInspector] private float _currentHealth;

        private bool _isDied;

        private void Start()
        {
            _weaponMovementController = GetComponent<WeaponMovementController>();
            _weaponShootingController = GetComponent<WeaponShootingController>();

            _currentHealth = _maxHealth;
            PlayerHealthUI.Instance.UpdateHealthBar(_currentHealth / _maxHealth, false);
        }

        public Transform GetPlayerTr() => transform;

        public void TakeDamage(float amount)
        {
            if (_isDied) return;

            _currentHealth -= amount;
            PlayerHealthUI.Instance.UpdateHealthBar(_currentHealth / _maxHealth);
            if (_currentHealth <= 0)
            {
                _isDied = true;
                GameManager.Instance.LevelFail();
            }
        }

        public Transform GetFirePoint() => _weaponShootingController.FirePointTr;
    }
}