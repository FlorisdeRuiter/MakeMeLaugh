using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] private Transform armTarget, raisedTarget;
    [SerializeField] private float grabDistance, raiseForce;
    [SerializeField] private LayerMask grabMask;
    private Rigidbody targetBody;
    private GameObject grabbedObject;

    private void Awake()
    {
        targetBody = armTarget.GetComponent<Rigidbody>();
    }

    public void RaiseArm()
    {
        targetBody.velocity = (raisedTarget.position - armTarget.position).normalized * raiseForce;
    }

    public void GrabItem()
    {
        if (grabbedObject != null)
            return;

        Collider[] collis = Physics.OverlapSphere(armTarget.position, grabDistance, grabMask);

        if (collis.Count() == 0)
            return;

        //cast sphere
        Collider closest = null;
        foreach (Collider collider in collis)
        {
            if (closest == null || Vector3.Distance(collider.transform.position, armTarget.position) > Vector3.Distance(closest.transform.position, armTarget.position))
            {
                closest = collider;
            }
        }

        //turn off it's physics and grab it
        grabbedObject = closest.gameObject;
        grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
        grabbedObject.transform.parent = armTarget.transform;
    }

    public void DropItem()
    {
        if (grabbedObject == null)
            return;

        grabbedObject.transform.parent = null;
        grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        grabbedObject.GetComponent<Rigidbody>().AddForce(raisedTarget.forward * 50, ForceMode.Impulse);
        grabbedObject = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(armTarget.position, grabDistance);
    }
}
