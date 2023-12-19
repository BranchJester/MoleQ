using System;
using System.Linq;
using MoleQ.Application.Extensions;
using MoleQ.Application.ServiceInjector;
using MoleQ.Application.UI.Notification;
using MoleQ.Core.Application.Interfaces.Player;
using MoleQ.Core.Domain.Enums;
using MoleQ.UI.Items;
using MoleQ.UI.Menus.Abstract;

namespace MoleQ.UI.Menus.Player;

public class PlayerMenu : BaseMenu
{
    private readonly IPlayerService _playerService;
    private readonly ISuperPunchService _superPunchService;
    private readonly ISuperRunService _superRunService;

    public PlayerMenu(string menuName, IPlayerService playerService, ISuperRunService superRunService,
        ISuperPunchService superPunchService) : base(menuName)
    {
        _playerService = playerService;
        _superRunService = superRunService;
        _superPunchService = superPunchService;
    }

    protected override void InitializeItems()
    {
        AddSubMenu(Injector.MenuManager.TeleportMenu, "Menu");
        FixPlayer();
        WantedLevel();
        LockWantedLevel();
        Invincible();
        InfiniteStamina();
        InfiniteBreath();
        InfiniteSpecialAbility();
        SuperJump();
        SuperRun();
        SuperPunch();
    }

    private void FixPlayer()
    {
        var fixPlayer =
            new CustomNativeItem(PlayerEnum.FixPlayer,
                HotkeysService.GetValueAsString(SectionEnum.Player, PlayerEnum.FixPlayer));
        fixPlayer.Activated += (_, _) => { _playerService.FixPlayer(); };
        _playerService.FixPlayerActivated += () => Notify.Message("Player Fixed");
        Add(fixPlayer);
    }

    private void WantedLevel()
    {
        var wantedLevel =
            new CustomNativeListItem<int>(PlayerEnum.WantedLevel,
                Enumerable.Range(0, 6).ToArray());
        wantedLevel.ItemChanged += (_, _) =>
        {
            _playerService.WantedLevel = wantedLevel.SelectedItem;
            _playerService.MaxWantedLevel = wantedLevel.SelectedItem;
        };
        _playerService.WantedLevelChanged += level =>
        {
            wantedLevel.SelectedItem = level;
            Notify.Message($"Wanted Level: {level}");
        };
        Add(wantedLevel);
    }

    private void LockWantedLevel()
    {
        var lockWantedLevel = new CustomNativeCheckboxItem(PlayerEnum.LockWantedLevel,
            HotkeysService.GetValueAsString(SectionEnum.Player, PlayerEnum.LockWantedLevel),
            _playerService.LockWantedLevel, state => _playerService.LockWantedLevel = state);
        _playerService.LockWantedLevelChanged += state =>
        {
            lockWantedLevel.Checked = state;
            Notify.CheckboxMessage("Wanted Level Locked", state);
        };
        Add(lockWantedLevel);
    }

    private void Invincible()
    {
        var invincible = new CustomNativeCheckboxItem(PlayerEnum.Invincible,
            HotkeysService.GetValueAsString(SectionEnum.Player, PlayerEnum.Invincible), _playerService.Invincible,
            state => _playerService.Invincible = state);
        _playerService.InvincibleChanged += state =>
        {
            invincible.Checked = state;
            Notify.CheckboxMessage("Invincibility", state);
        };
        Add(invincible);
    }

    private void InfiniteStamina()
    {
        var infiniteStamina = new CustomNativeCheckboxItem(PlayerEnum.InfiniteStamina,
            HotkeysService.GetValueAsString(SectionEnum.Player, PlayerEnum.InfiniteStamina),
            _playerService.InfiniteStamina, state => _playerService.InfiniteStamina = state);
        _playerService.InfiniteStaminaChanged += state =>
        {
            infiniteStamina.Checked = state;
            Notify.CheckboxMessage("Infinite Stamina", state);
        };
        Add(infiniteStamina);
    }

    private void InfiniteBreath()
    {
        var infiniteBreath = new CustomNativeCheckboxItem(PlayerEnum.InfiniteBreath,
            HotkeysService.GetValueAsString(SectionEnum.Player, PlayerEnum.InfiniteBreath),
            _playerService.InfiniteBreath, state => _playerService.InfiniteBreath = state);
        _playerService.InfiniteBreathChanged += state =>
        {
            infiniteBreath.Checked = state;
            Notify.CheckboxMessage("Infinite Breath", state);
        };
        Add(infiniteBreath);
    }

    private void InfiniteSpecialAbility()
    {
        var infiniteSpecialAbility = new CustomNativeCheckboxItem(PlayerEnum.InfiniteSpecialAbility,
            HotkeysService.GetValueAsString(SectionEnum.Player, PlayerEnum.InfiniteSpecialAbility),
            _playerService.InfiniteSpecialAbility, state => _playerService.InfiniteSpecialAbility = state);
        _playerService.InfiniteSpecialAbilityChanged += state =>
        {
            infiniteSpecialAbility.Checked = state;
            Notify.CheckboxMessage("Infinite Special Ability", state);
        };
        Add(infiniteSpecialAbility);
    }

    private void SuperJump()
    {
        var superJump =
            new CustomNativeCheckboxItem(PlayerEnum.SuperJump,
                HotkeysService.GetValueAsString(SectionEnum.Player, PlayerEnum.SuperJump), _playerService.SuperJump,
                state => _playerService.SuperJump = state);
        _playerService.SuperJumpChanged += state =>
        {
            superJump.Checked = state;
            Notify.CheckboxMessage("Super Jump", state);
        };
        Add(superJump);
    }

    private void SuperRun()
    {
        var superRun = new CustomNativeListItem<PlayerSuperSpeedEnum>(
            PlayerEnum.SuperRun, Enum.GetValues(typeof(PlayerSuperSpeedEnum))
                .Cast<PlayerSuperSpeedEnum>()
                .ToArray());
        superRun.Description =
            $"{PlayerEnum.SuperRun.GetDescription()}\n\n{superRun.SelectedItem.GetDescription()}";
        superRun.ItemChanged += (_, _) =>
        {
            superRun.Description =
                $"{PlayerEnum.SuperRun.GetDescription()}\n\n{superRun.SelectedItem.GetDescription()}";
            _superRunService.SuperRun = superRun.SelectedItem;
        };
        _superRunService.PlayerSuperSpeedChanged += speed =>
        {
            superRun.SelectedItem = speed;
            superRun.Description =
                $"{PlayerEnum.SuperRun.GetDescription()}\n\n{speed.GetDescription()}";
            Notify.Message($"Super Run ~w~set to ~g~{superRun.SelectedItem.ToString()}");
        };
        Add(superRun);
    }

    private void SuperPunch()
    {
        var superPunch = new CustomNativeCheckboxItem(PlayerEnum.SuperPunch,
            HotkeysService.GetValueAsString(SectionEnum.Player, PlayerEnum.SuperPunch), _superPunchService.SuperPunch,
            state => _superPunchService.SuperPunch = state);
        _superPunchService.SuperPunchChanged += state =>
        {
            superPunch.Checked = state;
            Notify.CheckboxMessage("Super Punch", state);
        };
        Add(superPunch);
    }
}