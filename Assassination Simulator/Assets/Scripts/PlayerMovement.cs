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

    private void OnMove(InputValue inputValue)
    {
        _movementInput = inputValue.Get<Vector2>().normalized;
    }

    private void OnSprint(InputValue inputValue)
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
