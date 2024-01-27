using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public void DamageEnemy()
    {
        Debug.Log("hit");
        GetComponent<EnemyBehaviour>().enabled = false;
        Invoke("Disappear", 3);
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}
