using System;
using GTA;
using MoleQ.Interfaces.Player;
using Newtonsoft.Json;

namespace MoleQ.Services.Player;

public class PlayerService : IPlayerService
{
    private Ped _character;
    private bool _infiniteStamina;
    private bool _invincible;
    private bool _lockWantedLevel;
    private bool _superJump;
    private int _wantedLevel;

    #region Properties

    public bool Invincible
    {
        get => _invincible;
        set
        {
            if (_invincible == value) return;
            _invincible = value;
            InvincibleChanged?.Invoke(value);
        }
    }

    public bool SuperJump
    {
        get => _superJump;
        set
        {
            _superJump = value;
            SuperJumpChanged?.Invoke(value);
        }
    }

    public int WantedLevel
    {
        get => _wantedLevel;
        set
        {
            if (_wantedLevel == value) return;
            _wantedLevel = value;
            WantedLevelChanged?.Invoke(value);
        }
    }

    public int MaxWantedLevel { get; set; }

    public bool LockWantedLevel
    {
        get => _lockWantedLevel;
        set
        {
            _lockWantedLevel = value;
            LockWantedLevelChanged?.Invoke(value);
        }
    }

    public bool InfiniteStamina
    {
        get => _infiniteStamina;
        set
        {
            _infiniteStamina = value;
            InfiniteStaminaChanged?.Invoke(value);
        }
    }

    [JsonIgnore]
    public Ped Character
    {
        get => _character;
        set
        {
            _character = value;
            CharacterChanged?.Invoke((PedHash)value.Model.Hash);
        }
    }

    #endregion

    #region Events

    public event Action FixPlayerActivated;
    public event Action<bool> InvincibleChanged;
    public event Action<int> WantedLevelChanged;
    public event Action<bool> LockWantedLevelChanged;
    public event Action<bool> InfiniteStaminaChanged;
    public event Action<PedHash> CharacterChanged;
    public event Action<bool> SuperJumpChanged;

    public void FixPlayer()
    {
        FixPlayerActivated?.Invoke();
    }

    #endregion
}