using System;
using MoleQ.Enums;
using MoleQ.Interfaces.Player;

namespace MoleQ.Services.Player;

public class SuperRunService : ISuperRunService
{
    private PlayerSuperSpeedEnum _playerSuperSpeed;

    public PlayerSuperSpeedEnum SuperRun
    {
        get => _playerSuperSpeed;
        set
        {
            if (_playerSuperSpeed == value) return;
            _playerSuperSpeed = value;
            PlayerSuperSpeedChanged?.Invoke(value);
        }
    }

    public event Action<PlayerSuperSpeedEnum> PlayerSuperSpeedChanged;
}