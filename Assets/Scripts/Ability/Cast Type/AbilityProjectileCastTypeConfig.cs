using Lean.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Winter Universe/Ability/Cast Type/New Projectile")]
    public class AbilityProjectileCastTypeConfig : AbilityCastTypeConfig
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public bool Homing { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public int Count { get; private set; }
        [field: SerializeField] public float Spread { get; private set; }

        public override void OnCast(Pawn caster, Pawn target, Vector3 position, Vector3 eulerAngles, Vector3 direction, List<AbilityHitTypeData> hitTypes, AbilityTargetType targetType)
        {
            Vector3 directionWithSpread;
            for (int i = 0; i < Count; i++)
            {
                directionWithSpread = eulerAngles + new Vector3(Random.Range(-Spread, Spread), Random.Range(-Spread, Spread), Random.Range(-Spread, Spread));
                LeanPool.Spawn(Prefab, position, Quaternion.LookRotation(directionWithSpread)).GetComponent<ProjectileController>().Initialize(caster, target, this, hitTypes, targetType);
            }
        }
    }
}