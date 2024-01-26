using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] private Transform hand, loweredTarget, raisedTarget, grabBox;
    [SerializeField] private Vector3 grabBoxSize;
    [SerializeField] private LayerMask grabMask;
    private GameObject grabbedObject;

    public void GrabItem()
    {
        if (grabbedObject != null)
            return;

        Collider[] collis = Physics.OverlapBox(grabBox.position, grabBoxSize / 2, Quaternion.identity, grabMask);

        if (collis.Count() == 0)
            return;

        Collider closest = null;
        foreach (Collider collider in collis)
        {
            if (collider.GetComponent<Rigidbody>() != null)
            {
                if (closest == null || Vector3.Distance(collider.transform.position, grabBox.position) > Vector3.Distance(closest.transform.position, grabBox.position))
                {
                    closest = collider;
                }
            }
        }

        //turn off it's physics and grab it
        Debug.Log("grabbing");
        grabbedObject = closest.gameObject;
        Rigidbody rBody;
        grabbedObject.TryGetComponent<Rigidbody>(out rBody);
        if (rBody == null)
            return;
        rBody.isKinematic = true;
        grabbedObject.transform.parent = grabBox.transform;

        Debug.Log("raising");
        hand.position = raisedTarget.position;
        grabbedObject.transform.position = hand.position;
    }

    public void ThrowItem()
    {
        if (grabbedObject == null)
            return;

        grabbedObject.transform.parent = null;
        grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
        grabbedObject.GetComponent<Rigidbody>().AddForce(raisedTarget.forward * 50, ForceMode.Impulse);
        grabbedObject = null;

        hand.position = loweredTarget.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(grabBox.position, grabBoxSize);
    }
}
