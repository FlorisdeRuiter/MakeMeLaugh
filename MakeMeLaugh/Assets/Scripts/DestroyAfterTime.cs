using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float timeTillDeath = 10f;

    private void start()
    {
        Invoke("Die", timeTillDeath);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
