using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private static EnemySpawner _instance;

    [Header("Enemy Spawn Offset")]
    [SerializeField] private Transform _mins, _maxs;

    private Dictionary<GameObject,ObjectPool> _enemyPools = new Dictionary<GameObject, ObjectPool>();

    public void SpawnEnemy(GameObject poolIndex)
    {

        ObjectPool pool;
        if (!_enemyPools.TryGetValue(poolIndex, out pool))
        {
            pool = new GameObject("pool").AddComponent<ObjectPool>();
            pool.PooledObject = poolIndex;
            pool.PoolSize = 100;
            pool._autoExpand = true;
            pool._expansionSize = 5;
            pool.transform.SetParent(transform);

            _enemyPools.Add(poolIndex, pool);
        }
        pool.GetPooledObject(GetRandomPosition(), Quaternion.identity, pool.transform);
    }

    public Vector3 GetRandomPosition()
    {
        Vector3 position = Vector3.zero;

        position.x = Random.Range(_mins.position.x, _maxs.position.x);
        position.z = Random.Range(_mins.position.z, _maxs.position.z);
        position.y = 1.5f;

        return position;
    }

    public static EnemySpawner GetInstance()
    {
        // Check if the instance exists
        if (_instance != null) return _instance;

        // Find a potential instance
        _instance = FindObjectOfType<EnemySpawner>();

        // If it's still null, then create a new one
        if (_instance == null)
        {
            _instance = new GameObject("GameManager").AddComponent<EnemySpawner>();
        }

        return _instance;
    }
}
