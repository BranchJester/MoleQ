using System;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using MoleQ.Constants;
using MoleQ.Enums;
using MoleQ.Interfaces.Player;
using MoleQ.ServiceInjector;
using MoleQ.Services.Player;
using MoleQ.Services.Settings;

namespace MoleQ.Scripts.Player;

public class PlayerBasicsScript : BaseScript
{
    private readonly IPlayerService _playerService;
    private readonly StorageService _storageService;

    public PlayerBasicsScript()
    {
        _playerService = Injector.PlayerService;
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
        
        var superPunch = HotkeysService.GetValue(SectionEnum.Player, PlayerEnum.SuperPunch);
        if (IsKeyPressed(superPunch))
            _playerService.SuperPunch = !_playerService.SuperPunch;
    }

    protected override void SaveSettings()
    {
        _storageService.SaveSettings(_playerService);
    }

    protected override void LoadSettings()
    {
        var settings = _storageService.LoadSettings<PlayerService>();
        _playerService.Invincible = settings.Invincible;
        _playerService.SuperJump = settings.SuperJump;
        _playerService.LockWantedLevel = settings.LockWantedLevel;
        _playerService.MaxWantedLevel = settings.MaxWantedLevel;
        _playerService.InfiniteStamina = settings.InfiniteStamina;
        _playerService.WantedLevel = settings.WantedLevel;
        _playerService.InfiniteBreath = settings.InfiniteBreath;
        _playerService.SuperPunch = settings.SuperPunch;
    }

    private void OnTick(object sender, EventArgs e)
    {
        Invincible();
        SuperJump();
        LockWantedLevel();
        InfiniteStamina();
        SuperPunch();
    }

    private void SuperPunch()
    {
        if (!_playerService.SuperPunch) return;
        if (!Game.Player.Character.IsInMeleeCombat) return;

        var nearbyEntities = World.GetNearbyEntities(Game.Player.Character.Position, 10.0f)
            .Where(e => e != Game.Player.Character);
        var firstEntity = nearbyEntities.OrderBy(e => Vector3.Distance(Game.Player.Character.Position, e.Position))
            .FirstOrDefault(e =>
                e.HasBeenDamagedBy(Game.Player.Character) && e.HasBeenDamagedBy(WeaponHash.Unarmed));


        if (firstEntity == null) return;
        firstEntity.ApplyForce(Game.Player.Character.ForwardVector * 10000.0f);
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