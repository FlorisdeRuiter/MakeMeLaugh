using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    private PlayerInput input;
    private Rigidbody rBody;
    private Vector2 inputVector;
    private bool isHoldingLeft, isHoldingRight, isRaisingLeft, isRaisingRight;

    [SerializeField] private float moveSpeed, turnSpeed;
    [Space]
    [SerializeField] private ArmController leftArm, rightArm;


    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        TurnCharacter();
        HandleArms();
    }

    private void ApplyMovement()
    {
        Vector3 translatedMovement = new Vector3(inputVector.x, rBody.velocity.y, inputVector.y).normalized * (moveSpeed * Time.deltaTime);
        rBody.velocity = translatedMovement;
    }

    private void TurnCharacter()
    {
        Vector3 moveForward = new Vector3(rBody.velocity.x, 0, rBody.velocity.z);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveForward, Vector3.up), turnSpeed);
    }

    private void HandleArms()
    {
        Debug.Log($"{isRaisingLeft}, {isRaisingRight}");
        if (isRaisingLeft)
            leftArm.RaiseArm();
        if (isRaisingRight)
            rightArm.RaiseArm();
    }

    #region readinputs
    public void OnMove(InputAction.CallbackContext context) { inputVector = context.ReadValue<Vector2>(); }
    public void OnRaiseLeft(InputAction.CallbackContext context) { isRaisingLeft = context.ReadValueAsButton(); }
    public void OnRaiseRight(InputAction.CallbackContext context) { isRaisingRight = context.ReadValueAsButton(); }
    public void OnHoldLeft(InputAction.CallbackContext context) { isHoldingLeft = context.ReadValueAsButton(); }
    public void OnHoldRight(InputAction.CallbackContext context) { isHoldingRight = context.ReadValueAsButton(); }
    #endregion
}
