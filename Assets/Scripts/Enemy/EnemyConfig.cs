using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Winter Universe/Enemy Config")]
    public class EnemyConfig : BasicInfoConfig
    {
        [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
        [field: SerializeField] public int Health { get; private set; } = 10;
        [field: SerializeField] public int Damage { get; private set; } = 2;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 3f;
        
        // Можно добавить другие параметры врага
    }
} 