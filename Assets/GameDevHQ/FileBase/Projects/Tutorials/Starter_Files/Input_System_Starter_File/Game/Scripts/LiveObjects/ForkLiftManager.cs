using Game.Scripts.LiveObjects;
using Game.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ForkLiftManager : MonoBehaviour
{
    [SerializeField]
    private Forklift _forkLift;
    private GameInput _input;
    
    void Start()
    {
        InitInput();
    }

    private void InitInput()
    {
        _input = new GameInput();
        _input.ForkLift.Enable();
        _input.ForkLift.Exit.performed += Exit_performed;
    }

    private void Exit_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _forkLift.Exit();
    }

    private void MoveFork(float forkValue)
    {
        if (forkValue > 0)
        {
            _forkLift.LiftFork();
        } 
        else if (forkValue < 0)
        {
            _forkLift.LowerFork();
        }
    }

    void Update()
    {
        var movement = _input.ForkLift.Movement.ReadValue<Vector2>();
        var forkValue = _input.ForkLift.Fork.ReadValue<float>();
        _forkLift.Move(movement);
        MoveFork(forkValue);
    }

    private void OnDisable()
    {
        _input.ForkLift.Disable();
        _input.ForkLift.Exit.performed -= Exit_performed;
    }
}
