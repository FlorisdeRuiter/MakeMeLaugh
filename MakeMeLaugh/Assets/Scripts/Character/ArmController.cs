using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    [SerializeField] private Transform armTarget, raisedTarget;
    [SerializeField] private float maxDistance, raiseForce;
    private Rigidbody targetBody;

    private void Awake()
    {
        targetBody = armTarget.GetComponent<Rigidbody>();
    }

    public void RaiseArm()
    {
        targetBody.velocity = (raisedTarget.position - armTarget.position).normalized * raiseForce;
    }
}
