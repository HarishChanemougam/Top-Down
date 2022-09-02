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

    Vector2 _playerMovement;

#if UNITY_EDITOR
    private void Reset()
    {
        _root = transform.parent;
        _speed = 5f;
        Debug.Log("reset");
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
        }
        else
        {
            _animator.SetBool("IsWalking", false); 
        }


        // if (direction.x > 0) 
        // {
        //     _root.rotation = Quaternion.Euler(0, 0, 0);
        // }
        // else if (direction.x < 0) 
        // {
        //     _root.rotation = Quaternion.Euler(0, 180, 0);
        // }

    }

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
}
