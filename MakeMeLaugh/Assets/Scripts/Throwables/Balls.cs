using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour, IThrowable
{
    private bool isActive = false;
    [SerializeField] private float yeetForce;

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
            Vector3 dir = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * yeetForce, ForceMode.Impulse);
            collision.gameObject.GetComponent<EnemyHealth>()?.DamageEnemy();
            Destroy(gameObject);
        }
    }
}
