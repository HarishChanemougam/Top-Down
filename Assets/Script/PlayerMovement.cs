
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] InputActionReference _moveInput;
    [SerializeField] InputActionReference _runInput;
    [SerializeField] InputActionReference _SummingInput;
    [SerializeField] InputActionReference _JumpInput;
    [SerializeField] InputActionReference _ghostInput;
    [SerializeField] InputActionReference _flipInput;
    [SerializeField] Transform _root;
    [SerializeField] Animator _animator;
    [SerializeField] float _movingThreshold;
    [SerializeField] float _speed;
    [SerializeField] float _isRunning;

    string _player;
    float _speedOfMovementVariable;
    Vector2 _playerMovement;
    Vector2 _aimDirection;
    Vector2 _direction;



#if UNITY_EDITOR
    private void Reset()
    {
        _root = transform.parent;
        Debug.Log("reset");
        _speed = 5f;
    }
#endif

    private void Start()
    {
        _moveInput.action.started += StartMove;
        _moveInput.action.performed += UpdateMove;
        _moveInput.action.canceled += EndMove;
    }

    private void Update()
    {
        _root.transform.Translate(_playerMovement * Time.deltaTime * _speed, Space.World);

      

        if (_playerMovement.magnitude > _movingThreshold)
        {
            _animator.SetBool("IsWalking", true);
            _animator.SetFloat("Vertical", _playerMovement.x);
            _animator.SetFloat("Horizontal",_playerMovement.y);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }

    }

    /*private void FixedUpdate()
    {

        _animator.SetFloat("Horizontal", _aimDirection.x);
        _animator.SetFloat("vertical", _aimDirection.y);
        _animator.SetBool("IsMoving", _direction.magnitude > 0.1f);
        _animator.SetBool("IsRunning", _isRunning);

    }*/



    private void StartMove(InputAction.CallbackContext obj)
    {
        _playerMovement = obj.ReadValue<Vector2>();

    }

    private void UpdateMove(InputAction.CallbackContext obj)
    {
        _playerMovement = obj.ReadValue<Vector2>();

    }

    private void EndMove(InputAction.CallbackContext obj)
    {
        _playerMovement = new Vector2(0, 0);
    }

    /* Rigidbody2D body;

     float horizontal;
     float vertical;
     float moveLimiter = 0.7f;

     public float runSpeed = 20.0f;

     void Start()
     {
         body = GetComponent<Rigidbody2D>();
     }

     void Update()
     {

         horizontal = Input.GetAxisRaw("Horizontal");
         vertical = Input.GetAxisRaw("Vertical");
     }

     void FixedUpdate()
     {
         if (horizontal != -1 && vertical != -1)
         {

             horizontal *= moveLimiter;
             vertical *= moveLimiter;
         }

         body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
     }*/

    private PlayerInput playerInput;

    private void LateUpdate()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void UpdateRotation()
    {
        Vector2 moveAction = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector2 rotateAction = playerInput.actions["Rotate"].ReadValue<Vector2>();

        Vector3 moveInput = new Vector3(moveAction.x, 0f, moveAction.y);
        Vector3 rotateInput = new Vector3(rotateAction.x, 0f, rotateAction.y);

        transform.Translate(moveInput * _speedOfMovementVariable * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotateInput), _speedOfMovementVariable * Time.deltaTime);
    }
}
