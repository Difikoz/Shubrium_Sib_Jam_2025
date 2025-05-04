using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class BossController : EnemyController
    {
        [SerializeField] private List<EnemySpawnData> _phase00 = new();
        [SerializeField] private List<EnemySpawnData> _phase01 = new();
        [SerializeField] private List<EnemySpawnData> _phase02 = new();
        [SerializeField] private List<EnemySpawnData> _phase03 = new();

        private int _currentPhase;

        public override void ActivateComponent()
        {
            base.ActivateComponent();
            if(_phase00.Count > 0)
            {
                GameManager.StaticInstance.SpawnManager.SpawnEnemies(GameManager.StaticInstance.StageManager.CurrentStage, _phase00);
            }
        }

        public override void EnableComponent()
        {
            base.EnableComponent();
            Health.OnValueChanged += OnHealthChanged;
        }

        public override void DisableComponent()
        {
            Health.OnValueChanged -= OnHealthChanged;
            base.DisableComponent();
        }

        private void OnHealthChanged(int current, int max)
        {
            float percent = (float)current / max;
            switch (_currentPhase)
            {
                case 0:
                    if (percent < 0.75f)
                    {
                        GameManager.StaticInstance.SpawnManager.SpawnEnemies(GameManager.StaticInstance.StageManager.CurrentStage, _phase01);
                        _currentPhase++;
                        StartCoroutine(InvulnerableTimer());
                    }
                    break;
                case 1:
                    if (percent < 0.5f)
                    {
                        GameManager.StaticInstance.SpawnManager.SpawnEnemies(GameManager.StaticInstance.StageManager.CurrentStage, _phase02);
                        _currentPhase++;
                        StartCoroutine(InvulnerableTimer());
                    }
                    break;
                case 2:
                    if (percent < 0.25f)
                    {
                        GameManager.StaticInstance.SpawnManager.SpawnEnemies(GameManager.StaticInstance.StageManager.CurrentStage, _phase03);
                        _currentPhase++;
                        StartCoroutine(InvulnerableTimer());
                    }
                    break;
                case 3:

                    break;
            }
        }

        private IEnumerator InvulnerableTimer()
        {
            Health.AddInvulnerable();
            WaitForSeconds delay = new(0.5f);
            while (GameManager.StaticInstance.StageManager.CurrentStage.EnemyCount > 0)
            {
                yield return delay;
            }
            Health.RemoveInvulnerable();
        }
    }
}