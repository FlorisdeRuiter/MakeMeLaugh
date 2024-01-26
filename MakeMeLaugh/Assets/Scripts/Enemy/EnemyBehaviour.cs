using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : PoolItem
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rb;

    private void Start()
    {
        SetTarget();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_target == null)
            SetTarget();
        if (_target == null)
            return;

        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector3 dir = (_target.position - transform.position).normalized;
        Vector3 newPosition = transform.position + dir * _moveSpeed * Time.deltaTime;
        _rb.Move(newPosition, Quaternion.identity);
    }

    private void SetTarget()
    {
        _target = GetNearestPlayer(GameManager.instance._playerList);
    }

    public Transform GetNearestPlayer(List<Transform> playerList)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        // Takes the transform from nearby enemies
        foreach (Transform potentialTarget in playerList)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;

            // Compares distance to player
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}
