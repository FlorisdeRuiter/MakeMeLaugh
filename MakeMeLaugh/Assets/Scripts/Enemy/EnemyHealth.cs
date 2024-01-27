using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public void DamageEnemy()
    {
        GetComponent<EnemyBehaviour>().enabled = false;
        Invoke("Disappear", 3);
        DodgeballManager.instance.RemovePeasant(gameObject);
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}
