using System;
using MoleQ.Core.Application.Interfaces.Player;
using MoleQ.Core.Domain.Enums;

namespace MoleQ.Core.Application.Services.Player;

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