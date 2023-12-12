using System;
using MoleQ.Interfaces.Player;

namespace MoleQ.Services.Player;

public class SuperPunchService : ISuperPunchService
{
    private bool _superPunch;

    public bool SuperPunch
    {
        get => _superPunch;
        set
        {
            if (_superPunch == value) return;
            _superPunch = value;
            SuperPunchChanged?.Invoke(value);
        }
    }

    public event Action<bool> SuperPunchChanged;
}