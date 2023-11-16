using System;
using _Game.Managers;
using _Game.Scripts.Runtime.Ammo;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Runtime.Controllers
{
    public class WeaponShootingController : MonoBehaviour
    {
        [SerializeField] private float _delayTime = .5f;
        [SerializeField] private Transform _weaponTr;
        [SerializeField] private CameraShake _cameraShake;

        [Space(10)] [SerializeField] private BulletBase _bulletBasePrefab;
        [SerializeField] private Transform _firePointTr;
        [SerializeField] private ParticleSystem _muzzleFlash;
        [SerializeField] private ParticleSystem _shellEffect;

        [Space(10)] [SerializeField] private Transform _barrelTr;
        [SerializeField] private float _barrelRotationSpeed = 10;

        private bool _isFireEnabled;

        private float _timeCounter;

        public Transform FirePointTr => _firePointTr;

        private void Update()
        {
            if (GameManager.Instance.IsGameOver) return;

            _isFireEnabled = Input.GetMouseButton(0);

            if (CanFire())
            {
                Fire();
            }

            if (_isFireEnabled)
            {
                _barrelTr.Rotate(Vector3.up * (_barrelRotationSpeed * Time.deltaTime));
            }
        }

        private void Fire()
        {
            _muzzleFlash.Play();
            _shellEffect.Play();
            LeanPool.Spawn(_bulletBasePrefab, _firePointTr.position, _firePointTr.rotation);
            Shake();
        }

        private void Shake()
        {
            _weaponTr.transform.DOShakeScale(.08f, .05f);
            _cameraShake.ShakeCaller(0.2f, 0.1f);
        }

        private bool CanFire()
        {
            _timeCounter += Time.deltaTime;
            if (_timeCounter >= _delayTime && _isFireEnabled)
            {
                _timeCounter = 0;
                return true;
            }

            return false;
        }
    }
}