using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour, IThrowable
{
    private bool isActive = false;
    [SerializeField] private int uses = 2;

    public void Throw()
    {
        isActive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive)
            return;
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>()?.DamageEnemy();
            uses--;
            if (uses <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
