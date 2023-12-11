using System;
using MoleQ.Enums;
using MoleQ.Interfaces.Player;

namespace MoleQ.Services.Player;

public class SuperRunService : ISuperRunService
{
    private PlayerSuperSpeedEnum _playerSuperSpeed;

    public PlayerSuperSpeedEnum PlayerSuperSpeed
    {
        get => _playerSuperSpeed;
        set
        {
            _playerSuperSpeed = value;
            PlayerSuperSpeedChanged?.Invoke(value);
        }
    }

    public event Action<PlayerSuperSpeedEnum> PlayerSuperSpeedChanged;
}