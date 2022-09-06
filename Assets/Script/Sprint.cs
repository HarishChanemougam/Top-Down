using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.InputSystem.InputSettings;

public class Sprint : MonoBehaviour
{
    [SerializeField] InputActionReference _runInput;
    [SerializeField] Transform _root;
    [SerializeField] Animator _animator;
    [SerializeField] float _runningThreshold;
    [SerializeField] float _speed;



    bool _IsRunning;
    Vector2 _playerMovement;
    Vector2 mv;
    Vector3 _aimDirection;
    float _speedOfMovementVariable;

    private Animator anim;

#if UNITY_EDITOR
    private void Reset()
    {
        _root = transform.parent;
        Debug.Log("reset");
        _speed = 10f;
    }
#endif

    void Start()
    {
        anim = GetComponent<Animator>();
        _runInput.action.started += StartRun;
        _runInput.action.performed += UpdateRun;
        _runInput.action.canceled += EndRun;
    }


    private void Update()
    {
        _root.transform.Translate(_playerMovement * Time.deltaTime * _speed, Space.World);



        if (_playerMovement.magnitude > _runningThreshold)
        {
            _animator.SetBool("IsRunning", _aimDirection.magnitude > 0);
            _animator.SetFloat("Vertical", _aimDirection.x);
            _animator.SetFloat("Horizontal", _aimDirection.y);
            _animator.SetBool("IsRunning", _IsRunning);
        }

    }

    private void StartRun(InputAction.CallbackContext obj)
    {
        _playerMovement = obj.ReadValue<Vector2>();

    }

    private void UpdateRun(InputAction.CallbackContext obj)
    {
        _playerMovement = obj.ReadValue<Vector2>();

    }

    private void EndRun(InputAction.CallbackContext obj)
    {
        _playerMovement = new Vector2(0, 0);
       /* Vector3 direction = _aimDirection.direction;*/
    }

    private PlayerInput playerInput;

    private void LateUpdate()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void UpdateRotation()
    {
        Vector2 RunAction = playerInput.actions["Run"].ReadValue<Vector2>();
        Vector2 rotateAction = playerInput.actions["Rotate"].ReadValue<Vector2>();

        Vector3 RunInput = new Vector3(RunAction.x, 0f, RunAction.y);
        Vector3 rotateInput = new Vector3(rotateAction.x, 0f, rotateAction.y);

        transform.Translate(RunInput * _speedOfMovementVariable * Time.deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotateInput), _speedOfMovementVariable * Time.deltaTime);
    }
}
