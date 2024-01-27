using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : PoolItem
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody _rb;

    [SerializeField] private List<Transform> _movetargets;

    private void Start()
    {
        SetTartgetsList();
        SetNewTarget();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_target == null || Vector3.Distance(transform.position, _target.position) < 3)
        {
            SetNewTarget();
        }

        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, Time.deltaTime);
    }

    private void SetNewTarget()
    {
        int index = Random.Range(0, _movetargets.Count);
        while (_movetargets[index] == _target)
        {
            index = Random.Range(0, _movetargets.Count);
        }

        _target = _movetargets[index];
    }

    private void SetTartgetsList(List<Transform> targets)
    {
        _movetargets = targets;
    }
}
