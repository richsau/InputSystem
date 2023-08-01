using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneInput : MonoBehaviour
{

    private GameInput _input;
    private Vector3 _movement;
    
    private bool _rotateRight = false;
    private bool _rotateLeft = false;
    private bool _exit = false;

    void Start()
    {
        InitInput();
    }

    private void InitInput()
    {
        _input = new GameInput();
        _input.Drone.Enable();
        _input.Drone.RotateLeft.started += RotateLeft_started;
        _input.Drone.RotateLeft.canceled += RotateLeft_canceled;
        _input.Drone.RotateRight.started += RotateRight_started;
        _input.Drone.RotateRight.canceled += RotateRight_canceled;
        _input.Drone.Exit.started += Exit_started;
        _input.Drone.Exit.canceled += Exit_canceled;
    }

    private void Exit_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _exit = false;
    }

    private void Exit_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _exit = true;
    }

    private void RotateRight_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _rotateRight = false;
    }

    private void RotateRight_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _rotateRight = true;
    }

    private void RotateLeft_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _rotateLeft = false;
    }

    private void RotateLeft_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _rotateLeft = true;
    }

    void Update()
    {
        _movement = _input.Drone.Movement.ReadValue<Vector3>();
    }

    public bool IsMovingBack()
    {
        if (_movement == Vector3.back)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public bool IsMovingForward()
    {
        if (_movement == Vector3.forward)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsMovingLeft()
    {
        if (_movement == Vector3.left)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsMovingRight()
    {
        if (_movement == Vector3.right)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsMovingUp()
    {
        if (_movement == Vector3.up)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsMovingDown()
    {
        if (_movement == Vector3.down)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsRotatingRight()
    {
        return _rotateRight;
    }

    public bool IsRotatingLeft()
    {
        return _rotateLeft;
    }

    public bool IsExiting()
    {
        return _exit;
    }

    private void OnDisable()
    {
        _input.Drone.Disable();
        _input.Drone.RotateLeft.started -= RotateLeft_started;
        _input.Drone.RotateLeft.canceled -= RotateLeft_canceled;
        _input.Drone.RotateRight.started -= RotateRight_started;
        _input.Drone.RotateRight.canceled -= RotateRight_canceled;
        _input.Drone.Exit.started -= Exit_started;
        _input.Drone.Exit.canceled -= Exit_canceled;
    }
}
