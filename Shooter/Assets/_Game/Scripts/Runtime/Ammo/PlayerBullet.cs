using System;
using _Game.Scripts.Runtime.Interface;
using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Runtime.Ammo
{
    public class PlayerBullet : BulletBase
    {
        [SerializeField] private float _damagePower;
        [SerializeField] private float _speed = 10;
        [SerializeField] private TrailRenderer _trailRenderer;

        [Space(10)] [SerializeField] private ParticleSystem _hitImpact;

        private void Update()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            _trailRenderer.Clear();

            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damagePower);
                LeanPool.Despawn(this);
                return;
            }

            LeanPool.Despawn(this);
            ParticleSystem impact = LeanPool.Spawn(_hitImpact, transform.position, _hitImpact.transform.rotation);
            LeanPool.Despawn(impact, .25f);
        }
    }
}