using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.LowLevel;

public class DodgeballManager : MonoBehaviour
{
    public static DodgeballManager instance;

    private CharacterController player1, player2;
    [SerializeField] private float peasantsPerSide;
    private List<GameObject> p1Peasants, p2Peasants;
    [SerializeField] private Transform p1Min, p1Max, p2Min, p2Max;
    [SerializeField] private GameObject p1PeasantPrefab, p2PeasantPrefab;
    [SerializeField] private List<Sprite> peasantSprites;

    [SerializeField] private float timeToHoldBall = 3;
    [SerializeField] private Transform ballAreaMin, ballAreaMax;

    [SerializeField] private UnityEvent p1WinEvent, p2WinEvent;

    [SerializeField] private List<Transform> p1TargetsList, p2TargetsList;
    [SerializeField] private Transform p1Spawn, p2Spawn, p1CamSpawn, p2CamSpawn;
    [SerializeField] private Camera p1Camera, p2Camera;

    [Space]
    [SerializeField] private GameObject p1JoinPrompt, p2JoinPrompt;
    [SerializeField] private GameObject p1WinPrompt, p2WinPrompt;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject restartButton;

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
            player1.relativeForward = p1Spawn.forward;
            player1.relativeRight = p1Spawn.right;
            p1Camera = player1.transform.parent.GetComponentInChildren<Camera>();
            player1.transform.position = p1Spawn.transform.position;
            player1.transform.rotation = p1Spawn.transform.rotation;

            p1Camera.transform.position = p1CamSpawn.transform.position;
            p1Camera.transform.rotation = p1CamSpawn.transform.rotation;
            p1Camera.cullingMask = p1Camera.cullingMask & ~(1 << LayerMask.NameToLayer("P2Billboard"));

            Debug.Log("player 1 added");
            p1JoinPrompt.SetActive(false);
        }
        else
        {
            player2 = input.GetComponent<CharacterController>();
            player2.relativeForward = p2Spawn.forward;
            player2.relativeRight = p2Spawn.right;
            p2Camera = player2.transform.parent.GetComponentInChildren<Camera>();
            player2.transform.position = p2Spawn.transform.position;
            player2.transform.rotation = p2Spawn.transform.rotation;

            p2Camera.transform.position = p2CamSpawn.transform.position;
            p2Camera.transform.rotation = p2CamSpawn.transform.rotation;

            p2Camera.cullingMask = p2Camera.cullingMask & ~(1 << LayerMask.NameToLayer("P1Billboard"));

            Debug.Log("player 2 added");
            p2JoinPrompt.SetActive(false);
        }

        if (player1 != null && player2 != null)
        {
            title.SetActive(false);
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
            Billboard[] boards = peasant.GetComponentsInChildren<Billboard>();

            boards[0]._camera = p1Camera;
            boards[0].gameObject.layer = LayerMask.NameToLayer("P1Billboard");
            boards[1]._camera = p2Camera;
            boards[1].gameObject.layer = LayerMask.NameToLayer("P2Billboard");

            var spriteRenderers = peasant.GetComponentsInChildren<SpriteRenderer>();
            var sprite = peasantSprites[Random.Range(0, peasantSprites.Count)];
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.sprite = sprite;
            }
        }

        for (int i = 0; i < peasantsPerSide; i++)
        {
            Vector3 position = new Vector3(Random.Range(p2Min.position.x, p2Max.position.x), Random.Range(p2Min.position.y, p2Max.position.y), Random.Range(p2Min.position.z, p2Max.position.z));
            GameObject peasant = Instantiate(p2PeasantPrefab, position, Quaternion.identity);
            p2Peasants.Add(peasant);
            peasant.GetComponent<EnemyBehaviour>().SetTargetsList(p2TargetsList);
            Billboard[] boards = peasant.GetComponentsInChildren<Billboard>();

            boards[0]._camera = p1Camera;
            boards[0].gameObject.layer = LayerMask.NameToLayer("P1Billboard");
            boards[1]._camera = p2Camera;
            boards[1].gameObject.layer = LayerMask.NameToLayer("P2Billboard");

            var spriteRenderers = peasant.GetComponentsInChildren<SpriteRenderer>();
            var sprite = peasantSprites[Random.Range(0, peasantSprites.Count)];
            foreach (var spriteRenderer in spriteRenderers)
            {
                spriteRenderer.sprite = sprite;
            }
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

        float t = 1;
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

    public void ShowWinnerP1()
    {
        p1WinPrompt.SetActive(true);
        restartButton.SetActive(true);
        EventSystem.current.SetSelectedGameObject(restartButton);
    }

    public void ShowWinnerP2()
    {
        p2WinPrompt.SetActive(true);
        restartButton.SetActive(true);
        EventSystem.current.SetSelectedGameObject(restartButton);
    }
}
