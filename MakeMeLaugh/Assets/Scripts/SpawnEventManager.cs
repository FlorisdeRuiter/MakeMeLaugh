using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnEventManager : MonoBehaviour
{
    [SerializeField] private SpawnData _spawnData;

    [SerializeField] private EnemySpawner _enemySpawner;

    private GameManager _timer;
    private int _eventIndex;

    private void Start()
    {
        _timer = GameManager.instance;
    }

    private void Update()
    {
        if (_eventIndex >= _spawnData.SpawnEvents.Count)
            return;

        SpawnEvent spawnEvent = _spawnData.SpawnEvents[_eventIndex];
        if (_timer.Timer > spawnEvent.Time)
        {
            foreach (EnemySpawnData spawnData in spawnEvent.EnemySpawnData)
            {
                for (int i = 0; i < spawnData.AmountToSpawn; i++)
                {
                    if (spawnData.GameObjectToSpawn.tag == "Enemy")
                        _enemySpawner.SpawnEnemy(spawnData.GameObjectToSpawn);
                    else
                        Instantiate(spawnData.GameObjectToSpawn, _enemySpawner.GetRandomPosition(), Quaternion.identity);
                }
            }

            _eventIndex += 1;
        }
    }


}
