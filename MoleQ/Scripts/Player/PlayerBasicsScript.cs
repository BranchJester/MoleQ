using System;
using System.Collections.Generic;
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

        var infiniteSpecialAbilityKey = HotkeysService.GetValue(SectionEnum.Player, PlayerEnum.InfiniteSpecialAbility);
        if (IsKeyPressed(infiniteSpecialAbilityKey))
            _playerService.InfiniteSpecialAbility = !_playerService.InfiniteSpecialAbility;
    }

    protected override void SaveSettings()
    {
        var settings = ServiceSettingsMapper.ExtractSettings<PlayerSettings>(new Dictionary<Type, object>
        {
            { typeof(IPlayerService), _playerService },
            { typeof(ISuperPunchService), _superPunchService },
            { typeof(ISuperRunService), _superRunService }
        });
        _storageService.SaveSettings(settings);
    }

    protected override void LoadSettings()
    {
        var settings = _storageService.LoadSettings<PlayerSettings>();
        var services = new Dictionary<Type, object>
        {
            { typeof(IPlayerService), _playerService },
            { typeof(ISuperPunchService), _superPunchService },
            { typeof(ISuperRunService), _superRunService }
        };
        ServiceSettingsMapper.ApplySettings(settings, services);
    }

    private void OnTick(object sender, EventArgs e)
    {
        Invincible();
        SuperJump();
        LockWantedLevel();
        InfiniteStamina();
        InfiniteSpecialAbility();
    }

    private void InfiniteSpecialAbility()
    {
        if (_playerService.InfiniteSpecialAbility && Game.Player.IsSpecialAbilityActive)
            Game.Player.RefillSpecialAbility();
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