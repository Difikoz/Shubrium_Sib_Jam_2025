using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;

        private Pawn _caster;
        private Pawn _target;
        private AbilityProjectileCastTypeConfig _config;
        private List<AbilityHitTypeData> _hitTypes;
        private AbilityTargetType _targetType;

        public void Initialize(Pawn caster, Pawn target, AbilityProjectileCastTypeConfig config, List<AbilityHitTypeData> hitTypes, AbilityTargetType targetType)
        {
            _caster = caster;
            _target = target;
            _config = config;
            _hitTypes = new(hitTypes);
            _targetType = targetType;
            StartCoroutine(DespawnTimer());
            _rb.linearVelocity = transform.forward * _config.Speed;
        }

        private IEnumerator DespawnTimer()
        {
            yield return new WaitForSeconds(_config.Distance / _config.Speed);
            Despawn(null);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_rb.linearVelocity != Vector3.zero)
            {
                Despawn(other);
            }
        }

        private void Despawn(Collider other)
        {
            foreach (AbilityHitTypeData hitTypeData in _hitTypes)
            {
                if (hitTypeData.Triggered)
                {
                    hitTypeData.HitType.OnHit(_caster, other, transform.position, transform.eulerAngles, transform.forward, _targetType);
                }
            }
            _rb.linearVelocity = Vector3.zero;
            LeanPool.Despawn(LeanPool.Spawn(_config.HitEffect, transform.position, Quaternion.identity), 10f);
            LeanPool.Despawn(gameObject);
        }
    }
}