using System;
using Lean.Pool;
using UnityEngine;

namespace _Game.Scripts.Runtime.Ammo
{
    public class PlayerBullet : BulletBase
    {
        [SerializeField] private float _speed = 10;
        [SerializeField] private TrailRenderer _trailRenderer;

        [Space(10)] [SerializeField] private ParticleSystem _hitImpact;

        private void Update()
        {
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            ParticleSystem impact = LeanPool.Spawn(_hitImpact, transform.position, _hitImpact.transform.rotation);
            _trailRenderer.Clear();
            LeanPool.Despawn(this);
            LeanPool.Despawn(impact, .25f);
        }
    }
}