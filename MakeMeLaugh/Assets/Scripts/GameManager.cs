using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public List<Transform> _playerList;

    public float Timer;
    [SerializeField] private float timeToWin = 60f;

    [SerializeField] GameObject selected;

    private void Awake()
    {
        //Time.timeScale = 0f;
        instance = this;
    }

    private void Start()
    {
        EventSystem eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(selected);
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= timeToWin)
            WinGame();

        Debug.Log(EventSystem.current.currentSelectedGameObject);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
    }

    public void WinGame()
    {
        Debug.Log("you win");
    }

    public void AddScore(float scoreToAdd)
    {
        Timer += scoreToAdd;
    }

    public float GetCurrentFill()
    {
        return Timer / timeToWin;
    }

    public void ShowItem(Sprite item)
    {
        UiManager.instance.SetItemAlpha(1);
        UiManager.instance.SetItemSprite(item);
        StartCoroutine(LowerItemOpacityOverTime());
    }

    private IEnumerator LowerItemOpacityOverTime()
    {
        float i = 1;
        while ( i > 0 )
        {
            UiManager.instance.SetItemAlpha(i);
            i -= Time.deltaTime / 7f;
            yield return null;
        }
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