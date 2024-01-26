using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerCameraUpdater : MonoBehaviour
{
    CinemachineTargetGroup targetGroup;

    private void Awake()
    {
        targetGroup = FindObjectOfType<CinemachineTargetGroup>();
    }

    public void AddPlayerToGroup(PlayerInput player)
    {
        targetGroup.AddMember(player.transform, 1, 1);
        GameManager.instance.AddPlayerToList(player);
    }

    public void RemovePlayerFromGroup(PlayerInput player)
    {
        targetGroup.RemoveMember(player.transform);
        GameManager.instance.RemovePlayerFromList(player);
    }
}
