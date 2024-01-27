using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour, IThrowable
{
    private bool isActive = false;
    [SerializeField] private float radius = 3f, yeetForce = 5;
    [SerializeField] private GameObject wineExplosionPrefab;

    public void Throw()
    {
        isActive = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isActive)
            return;

        Collider[] collis = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in collis)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                collider.gameObject.GetComponent<EnemyHealth>()?.DamageEnemy();
                Vector3 dir = (collider.transform.position - transform.position).normalized;
                collider.gameObject.GetComponent<Rigidbody>().AddForce(dir * yeetForce, ForceMode.Impulse);
            }
        }

        Instantiate(wineExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
