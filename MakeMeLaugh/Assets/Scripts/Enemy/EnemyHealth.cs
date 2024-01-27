using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float scoreForDeath = 3f;

    public void DamageEnemy()
    {
        Debug.Log("hit");
        GetComponent<EnemyBehaviour>().enabled = false;
        Invoke("Disappear", 3);
        GameManager.instance.AddScore(scoreForDeath);
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
}
