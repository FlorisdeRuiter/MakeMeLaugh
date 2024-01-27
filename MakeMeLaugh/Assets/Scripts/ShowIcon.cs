using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIcon : MonoBehaviour
{
    [SerializeField] private Sprite icon;

    private void Awake()
    {
        GameManager.instance.ShowItem(icon);
    }
}
