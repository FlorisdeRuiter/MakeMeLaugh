using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public float MaxTimer;
    public float TaskTimer;
    [SerializeField] private UnityEvent TaskCompleteEvent;
    [SerializeField] private float _timeBonus;

    [SerializeField] private List<ATask> _tasks;
    [SerializeField] private ATask _currentTask;
    [SerializeField] private List<Transform> _spawnPositions;

    private void Awake()
    {
        //Time.timeScale = 0f;
        instance = this;
    }

    private void Start()
    {
        TaskCompleteEvent.AddListener(FinishTask);
        TaskTimer = MaxTimer;
        StartTask();
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

    public void StartGame()
    {
        Time.timeScale = 1f;
    }

    private void StartTask()
    {
        _currentTask = SelectTask();
        Instantiate(_currentTask, _spawnPositions[Random.Range(0, _spawnPositions.Count)]);
    }

    private ATask SelectTask()
    {
        return _tasks[Random.Range(0, _tasks.Count)];
    }

    public void FinishTask()
    {
        TaskTimer += _timeBonus;
        _currentTask = null;
    }

    public void FailTask()
    {
        TaskTimer -= _timeBonus;
        _currentTask = null;
    }

    public float GetNormalizedTime()
    {
        return TaskTimer / MaxTimer;
    }
}