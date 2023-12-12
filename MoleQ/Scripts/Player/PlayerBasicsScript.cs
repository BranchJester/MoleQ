using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using MoleQ.Constants;
using MoleQ.Enums;
using MoleQ.Interfaces.Player;
using MoleQ.ServiceInjector;
using MoleQ.Services.Settings;
using MoleQ.Settings;

namespace MoleQ.Scripts.Player;

public class PlayerBasicsScript : BaseScript
{
    private readonly IPlayerService _playerService;
    private readonly StorageService _storageService;
    private readonly ISuperPunchService _superPunchService;
    private readonly ISuperRunService _superRunService;

    public PlayerBasicsScript()
    {
        _playerService = Injector.PlayerService;
        _superPunchService = Injector.SuperPunchService;
        _superRunService = Injector.SuperRunService;
        _storageService = new StorageService($"{Path.Settings}/Player.json");
        _playerService.FixPlayerActivated += FixPlayer;
        _playerService.WantedLevelChanged += ChangeWantedLevel;
        _playerService.InfiniteBreathChanged += InfiniteBreath;

        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    private void InfiniteBreath(bool infiniteBreath)
    {
        Game.Player.Character.DrownsInWater = !infiniteBreath;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        var fixPlayerKey = HotkeysService.GetValue(SectionEnum.Player, PlayerEnum.FixPlayer);
        if (IsKeyPressed(fixPlayerKey))
            _playerService.FixPlayer();

        var invincibleKey = HotkeysService.GetValue(SectionEnum.Player, PlayerEnum.Invincible);
        if (IsKeyPressed(invincibleKey))
            _playerService.Invincible = !_playerService.Invincible;

        var superJumpKey = HotkeysService.GetValue(SectionEnum.Player, PlayerEnum.SuperJump);
        if (IsKeyPressed(superJumpKey))
            _playerService.SuperJump = !_playerService.SuperJump;

        var lockWantedLevelKey = HotkeysService.GetValue(SectionEnum.Player, PlayerEnum.LockWantedLevel);
        if (IsKeyPressed(lockWantedLevelKey))
            _playerService.LockWantedLevel = !_playerService.LockWantedLevel;

        var infiniteStaminaKey = HotkeysService.GetValue(SectionEnum.Player, PlayerEnum.InfiniteStamina);
        if (IsKeyPressed(infiniteStaminaKey))
            _playerService.InfiniteStamina = !_playerService.InfiniteStamina;

        var infiniteBreath = HotkeysService.GetValue(SectionEnum.Player, PlayerEnum.InfiniteBreath);
        if (IsKeyPressed(infiniteBreath))
            _playerService.InfiniteBreath = !_playerService.InfiniteBreath;
    }

    protected override void SaveSettings()
    {
        var playerSettings = new PlayerSettings
        {
            Invincible = _playerService.Invincible,
            SuperJump = _playerService.SuperJump,
            LockWantedLevel = _playerService.LockWantedLevel,
            MaxWantedLevel = _playerService.MaxWantedLevel,
            InfiniteStamina = _playerService.InfiniteStamina,
            WantedLevel = _playerService.WantedLevel,
            InfiniteBreath = _playerService.InfiniteBreath,
            SuperPunch = _superPunchService.SuperPunch,
            SuperRun = _superRunService.SuperRun
        };
        _storageService.SaveSettings(playerSettings);
    }

    protected override void LoadSettings()
    {
        var settings = _storageService.LoadSettings<PlayerSettings>();
        _playerService.Invincible = settings.Invincible;
        _playerService.SuperJump = settings.SuperJump;
        _playerService.LockWantedLevel = settings.LockWantedLevel;
        _playerService.MaxWantedLevel = settings.MaxWantedLevel;
        _playerService.InfiniteStamina = settings.InfiniteStamina;
        _playerService.WantedLevel = settings.WantedLevel;
        _playerService.InfiniteBreath = settings.InfiniteBreath;
        _superPunchService.SuperPunch = settings.SuperPunch;
    }

    private void OnTick(object sender, EventArgs e)
    {
        Invincible();
        SuperJump();
        LockWantedLevel();
        InfiniteStamina();
    }

    private void InfiniteStamina()
    {
        if (Game.Player.RemainingSprintTime > 10.00f) return;

        if (_playerService.InfiniteStamina)
            Function.Call(Hash.RESET_PLAYER_STAMINA, Game.Player);
    }

    private void LockWantedLevel()
    {
        var wantedLevelIsDropping = Function.Call<bool>(Hash.ARE_PLAYER_FLASHING_STARS_ABOUT_TO_DROP, Game.Player);
        if (_playerService.LockWantedLevel && wantedLevelIsDropping)
            Game.Player.WantedLevel = _playerService.MaxWantedLevel;
    }

    private void ChangeWantedLevel(int wantedLevel)
    {
        Game.Player.WantedLevel = wantedLevel;
    }

    private void FixPlayer()
    {
        Game.Player.Character.Health = Game.Player.Character.MaxHealth;
        Game.Player.Character.Armor = Game.Player.MaxArmor;
        Game.Player.RefillSpecialAbility();
    }

    private void SuperJump()
    {
        if (_playerService.SuperJump)
            Game.Player.SetSuperJumpThisFrame();
    }

    private void Invincible()
    {
        if (Game.Player.Character.IsInvincible != _playerService.Invincible)
            Game.Player.Character.IsInvincible = _playerService.Invincible;
    }
}