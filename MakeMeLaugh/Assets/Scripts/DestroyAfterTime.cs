using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    private void Awake()
    {
        Invoke("Die", 10f);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
