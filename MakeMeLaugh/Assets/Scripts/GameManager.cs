using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public float MaxTimer;
    public float TaskTimer;
    [SerializeField] private UnityEvent TaskCompleteEvent;
    [SerializeField] private float _timeBonus;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TaskCompleteEvent.AddListener(FinishTask);
        TaskTimer = MaxTimer;
    }

    private void Update()
    {
        TaskTimer -= Time.deltaTime;
        if (TaskTimer > MaxTimer)
            TaskTimer = MaxTimer;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TaskCompleteEvent?.Invoke();
        }
    }

    private void FinishTask()
    {
        TaskTimer += _timeBonus;
    }

    public float GetNormalizedTime()
    {
        return TaskTimer / MaxTimer;
    }
}