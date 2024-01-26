using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATask : MonoBehaviour
{
    [SerializeField] private float _taskTime;
    public Sprite _taskSprite;

    private void Update()
    {
        _taskTime -= Time.deltaTime;

        if (_taskTime > 0)
            return;

        Fail();
    }

    public void Complete()
    {
        GameManager.instance.FinishTask();
        Destroy(gameObject);
    }

    public void Fail()
    {
        GameManager.instance.FailTask();
        Destroy(gameObject);
    }
}
