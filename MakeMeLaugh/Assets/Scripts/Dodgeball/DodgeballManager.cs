using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DodgeballManager : MonoBehaviour
{
    public static DodgeballManager instance;

    private CharacterController player1, player2;
    [SerializeField] private float peasantsPerSide;
    private List<GameObject> p1Peasants, p2Peasants;
    [SerializeField] private Transform p1Min, p1Max, p2Min, p2Max;
    private GameObject p1PeasantPrefab, p2PeasantPrefab;

    [SerializeField] private float timeToHoldBall = 3;
    [SerializeField] private Transform ballAreaMin, ballAreaMax;

    [SerializeField] private UnityEvent p1WinEvent, p2WinEvent;

    [SerializeField] private List<Transform> p1TargetsList, p2TargetsList;
    [SerializeField] private Transform p1Spawn, p2Spawn, p1Cam, p2Cam;

    private void Awake()
    {
        instance = this;
        p1Peasants = new List<GameObject>();
        p2Peasants = new List<GameObject>();
    }

    public void AddPlayer(PlayerInput input)
    {

        if (player1 == null)
        {
            player1 = input.GetComponent<CharacterController>();
            Camera playerCamera = player1.transform.parent.GetComponentInChildren<Camera>();
            player1.transform.position = p1Spawn.transform.position;
            player1.transform.rotation = p2Spawn.transform.rotation;
            playerCamera.transform.position = p1Cam.transform.position;
            playerCamera.transform.rotation = p1Cam.transform.rotation;
            Debug.Log("player 1 added");
        }
        else
        {
            player2 = input.GetComponent<CharacterController>();
            Camera playerCamera = player2.transform.parent.GetComponentInChildren<Camera>();
            player2.transform.position = p2Spawn.transform.position;
            player2.transform.rotation = p2Spawn.transform.rotation;
            playerCamera.transform.position = p2Cam.transform.position;
            playerCamera.transform.rotation = p2Cam.transform.rotation;
            Debug.Log("player 2 added");
        }

        if (player1 != null && player2 != null)
        {
            Destroy(FindObjectOfType<PlayerInputManager>());
            StartCoroutine(RunCountdown());
        }
    }

    private void SpawnPeasants()
    {
        for (int i = 0; i < peasantsPerSide; i++)
        {
            Vector3 position = new Vector3(Random.Range(p1Min.position.x, p1Max.position.x), Random.Range(p1Min.position.y, p1Max.position.y), Random.Range(p1Min.position.z, p1Max.position.z));
            GameObject peasant = Instantiate(p1PeasantPrefab, position, Quaternion.identity);
            p1Peasants.Add(peasant);
            peasant.GetComponent<EnemyBehaviour>().SetTargetsList(p1TargetsList);
        }

        for (int i = 0; i < peasantsPerSide; i++)
        {
            Vector3 position = new Vector3(Random.Range(p2Min.position.x, p2Max.position.x), Random.Range(p2Min.position.y, p2Max.position.y), Random.Range(p2Min.position.z, p2Max.position.z));
            GameObject peasant = Instantiate(p2PeasantPrefab, position, Quaternion.identity);
            p2Peasants.Add(peasant);
            peasant.GetComponent<EnemyBehaviour>().SetTargetsList(p2TargetsList);
        }
    }

    public void RemovePeasant(GameObject peasant)
    {
        if (p1Peasants.Contains(peasant))
        {
            p1Peasants.Remove(peasant);
            if (p1Peasants.Count <= 0)
            {
                p2WinEvent?.Invoke();
            }
        }

        if (p2Peasants.Contains(peasant))
        {
            p2Peasants.Remove(peasant);
            if (p2Peasants.Count <= 0)
            {
                p1WinEvent?.Invoke();
            }
        }
    }

    private void HoldBallOutOfPlay(GameObject ball)
    {
        StartCoroutine(HoldBalls(ball));
    }

    public IEnumerator HoldBalls(GameObject ball)
    {
        ball.transform.position = new Vector3(-300, -300, -300);
        float t = 0f;
        while (t < timeToHoldBall)
        {
            yield return null;
            t += Time.deltaTime;
        }
        ball.transform.position = new Vector3(Random.Range(ballAreaMin.position.x, ballAreaMax.position.x), Random.Range(ballAreaMin.position.y, ballAreaMax.position.y), Random.Range(ballAreaMin.position.z, ballAreaMax.position.z));
        Rigidbody rBody = ball.GetComponent<Rigidbody>();
        rBody.velocity = Vector3.zero;
        rBody.angularVelocity = Vector3.zero;
    }

    private IEnumerator RunCountdown()
    {
        player1.isLocked = true;
        player2.isLocked = true;
        SpawnPeasants();

        float t = 3;
        while (t > 0)
        {
            t -= Time.deltaTime;
            ShowCountdown(Mathf.CeilToInt(t));
            yield return null;
        }

        ShowCountdown(0);
        player1.isLocked = false;
        player2.isLocked = false;
    }

    private void ShowCountdown(int number)
    {
        //send to uimanager, make it visual
    }
}
