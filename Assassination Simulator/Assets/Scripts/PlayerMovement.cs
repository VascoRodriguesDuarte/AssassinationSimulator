using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float sprintValue;

    private Rigidbody2D _rigidbody;
    private float sprint = 0f;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Vector2 _movementInput;

    // Start is called before the first frame update
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }


    private void OnEnable()
    {
        PlayerInputGame.MoveInput += Movement;
        PlayerInputGame.SprintInput += ExtraMovement;
    }

    private void OnDisable()
    {
        PlayerInputGame.MoveInput -= Movement;
        PlayerInputGame.SprintInput -= ExtraMovement;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp( 
            _smoothedMovementInput,
            _movementInput, 
            ref _movementInputSmoothVelocity, 
            0.1f);

        _rigidbody.velocity = _smoothedMovementInput * (speed + sprint);
    }

    private void Movement(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>().normalized;
    }

    private void ExtraMovement(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            sprint = sprintValue;
        }
        else
        {
            sprint = 0f;
        }
    }
}
