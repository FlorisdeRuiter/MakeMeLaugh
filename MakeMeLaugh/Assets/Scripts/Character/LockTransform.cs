using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockTransform : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Update()
    {
        target.position = transform.position;
    }
}
