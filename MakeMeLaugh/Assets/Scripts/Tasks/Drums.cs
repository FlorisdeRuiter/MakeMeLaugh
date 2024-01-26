using UnityEngine;
using UnityEngine.Events;

public class Drums : ATask
{
    [SerializeField] private int TotalBonks;
    [SerializeField] private UnityEvent _bonkEvent;
    private int _bonkCount;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Grabbable/DrumSticks"))
            return;

        AddBonk();
        _bonkEvent?.Invoke();
    }

    private void AddBonk()
    {
        _bonkCount++;

        if (_bonkCount >= TotalBonks)
        {
            Complete();
        }
    }
}