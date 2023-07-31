using Game.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    private GameInput _input;

    void Start()
    {
        InitInput();
    }

    void Update()
    {
        var movement = _input.Player.Movement.ReadValue<Vector2>();
        _player.Move(movement);
    }

    private void InitInput()
    {
        _input = new GameInput();
        _input.Player.Enable();
    }
}
