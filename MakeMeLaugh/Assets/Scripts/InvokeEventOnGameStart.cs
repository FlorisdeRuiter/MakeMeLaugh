using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InvokeEventOnGameStart : MonoBehaviour
{
    [SerializeField] private UnityEvent _startEvent;

    void Awake()
    {
        _startEvent?.Invoke();
    }
}
