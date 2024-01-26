using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
    private Rigidbody rBody;
    private Vector2 inputVector;
    private bool isJumping, isHoldingLeft, isHoldingRight, isRaisingLeft, isRaisingRight;

    [SerializeField] private float moveSpeed, turnSpeed;
    [Space]
    [SerializeField] private float jumpForce, groundRayLength;
    [SerializeField] private LayerMask floorMask;
    private bool isGrounded;
    [Space]
    [SerializeField] private float armRaiseSpeed;


    private void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundRayLength, floorMask);
        Debug.Log(isJumping);
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        TurnCharacter();
        HandleArms();
    }

    private void ApplyMovement()
    {
        Vector3 translatedMovement = new Vector3(inputVector.x, 0, inputVector.y).normalized * (moveSpeed / Time.deltaTime);
        if (isGrounded && isJumping)
        {
            translatedMovement.y = jumpForce;
            isGrounded = false;
        }

        rBody.velocity = translatedMovement;
    }

    private void TurnCharacter()
    {

    }

    private void HandleArms()
    {

    }

    #region readinputs
    public void OnMove(InputAction.CallbackContext context) { inputVector = context.ReadValue<Vector2>(); Debug.Log("moving"); }
    public void floomp() { Debug.Log("Floomp"); }
    public void OnJump(InputAction.CallbackContext context) { isJumping = context.ReadValueAsButton(); }
    public void OnRaiseLeft(InputAction.CallbackContext context) { isRaisingLeft = context.ReadValueAsButton(); }
    public void OnRaiseRight(InputAction.CallbackContext context) { isRaisingRight = context.ReadValueAsButton(); }
    public void OnHoldLeft(InputAction.CallbackContext context) { isHoldingLeft = context.ReadValueAsButton(); }
    public void OnHoldRight(InputAction.CallbackContext context) { isHoldingRight = context.ReadValueAsButton(); }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * groundRayLength));
    }
}
