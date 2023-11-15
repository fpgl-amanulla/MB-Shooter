using System;
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
            _currentHealth -= amount;
            PlayerHealthUI.Instance.UpdateHealthBar(_currentHealth / _maxHealth);
        }
    }
}