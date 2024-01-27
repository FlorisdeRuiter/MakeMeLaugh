using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DodgeBallSignupSheet : MonoBehaviour
{
    public void RegisterPlayer(PlayerInput input)
    {
        DodgeballManager.instance.AddPlayer(input);
    }
}
