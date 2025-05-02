using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [Serializable]
    public class EnemySpawnData
    {
        public EnemyConfig EnemyConfig;
        public Vector3 SpawnPosition;
        public Quaternion SpawnRotation = Quaternion.identity;
    }
    
    [CreateAssetMenu(fileName = "Level", menuName = "Winter Universe/Level Config")]
    public class LevelConfig : BasicInfoConfig
    {        
        [field: SerializeField] public List<EnemySpawnData> EnemiesToSpawn { get; private set; } = new List<EnemySpawnData>();
    }
} 