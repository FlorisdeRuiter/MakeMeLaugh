using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public List<Transform> _playerList;

    public float Timer;

    private void Awake()
    {
        //Time.timeScale = 0f;
        instance = this;
    }

    private void Update()
    {
        Timer += Time.deltaTime;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
    }

    public void AddPlayerToList(PlayerInput player)
    {
        _playerList.Add(player.transform);
    }

    public void RemovePlayerFromList(PlayerInput player)
    {
        _playerList.Remove(player.transform);
    }
}