using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Winter Universe/AI/New Enemy")]
    public class EnemyConfig : BasicInfoConfig
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }
    }
} 