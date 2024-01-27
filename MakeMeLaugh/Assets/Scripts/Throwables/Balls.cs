using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour, IThrowable
{
    private bool isActive = false;
    [SerializeField] private float yeetForce, timeTillInactive = 3;

    public void Throw()
    {
        isActive = true;
        Invoke("Deactivate", timeTillInactive);
    }

    public void Deactivate()
    {
        isActive = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive)
            return;
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 dir = (collision.transform.position - transform.position).normalized;
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(dir * yeetForce, ForceMode.Impulse);
            collision.gameObject.GetComponent<EnemyHealth>()?.DamageEnemy();
            DodgeballManager.instance.HoldBalls(gameObject);
        }
    }
}
