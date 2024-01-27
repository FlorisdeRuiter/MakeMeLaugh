using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] UnityEvent deathEvent;
    [SerializeField] private Animator animator;
    bool isAlive = true;

    public void DamageEnemy()
    {
        isAlive = false;
        GetComponent<EnemyBehaviour>().enabled = false;
        Invoke("Disappear", 3);
        animator.SetTrigger("death");
        DodgeballManager.instance.RemovePeasant(gameObject);
        deathEvent.Invoke();
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }
}
