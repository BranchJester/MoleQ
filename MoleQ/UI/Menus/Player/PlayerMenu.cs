﻿using System;
using System.Linq;
using MoleQ.Enums;
using MoleQ.Extensions;
using MoleQ.Interfaces.Player;
using MoleQ.ServiceInjector;
using MoleQ.UI.Items;
using MoleQ.UI.Menus.Abstract;
using MoleQ.UI.Notification;

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
        SuperJump();
        SuperRun();
        InfiniteStamina();
        InfiniteBreath();
        SuperPunch();
    }

    private void SuperPunch()
    {
        var superPunch = new CustomNativeCheckboxItem(PlayerEnum.SuperPunch,
            HotkeysService.GetValueAsString(SectionEnum.Player, PlayerEnum.SuperPunch), _superPunchService.SuperPunch);
        superPunch.CheckboxChanged += (_, _) => { _superPunchService.SuperPunch = superPunch.Checked; };
        _superPunchService.SuperPunchChanged += state =>
        {
            superPunch.Checked = state;
            Notify.CheckboxMessage("Super Punch", state);
        };
        Add(superPunch);
    }

    private void InfiniteBreath()
    {
        var infiniteBreath = new CustomNativeCheckboxItem(PlayerEnum.InfiniteBreath,
            HotkeysService.GetValueAsString(SectionEnum.Player, PlayerEnum.InfiniteBreath),
            _playerService.InfiniteBreath);
        infiniteBreath.CheckboxChanged += (_, _) => { _playerService.InfiniteBreath = infiniteBreath.Checked; };
        _playerService.InfiniteBreathChanged += state =>
        {
            infiniteBreath.Checked = state;
            Notify.CheckboxMessage("Infinite Breath", state);
        };
        Add(infiniteBreath);
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
            _playerService.LockWantedLevel);
        lockWantedLevel.CheckboxChanged += (_, _) =>
        {
            _playerService.LockWantedLevel = lockWantedLevel.Checked;
            Notify.CheckboxMessage("Wanted Level Locked", lockWantedLevel.Checked);
        };
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
            HotkeysService.GetValueAsString(SectionEnum.Player, PlayerEnum.Invincible), _playerService.Invincible)
        {
            Checked = _playerService.Invincible
        };
        invincible.CheckboxChanged += (_, _) =>
        {
            _playerService.Invincible = invincible.Checked;
            Notify.CheckboxMessage("Invincibility", invincible.Checked);
        };
        _playerService.InvincibleChanged += state =>
        {
            invincible.Checked = state;
            Notify.CheckboxMessage("Invincibility", state);
        };
        Add(invincible);
    }

    private void SuperJump()
    {
        var superJump =
            new CustomNativeCheckboxItem(PlayerEnum.SuperJump,
                HotkeysService.GetValueAsString(SectionEnum.Player, PlayerEnum.SuperJump), _playerService.SuperJump)
            {
                Checked = _playerService.SuperJump
            };
        superJump.CheckboxChanged += (_, _) => { _playerService.SuperJump = superJump.Checked; };
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

    private void InfiniteStamina()
    {
        var infiniteStamina = new CustomNativeCheckboxItem(PlayerEnum.InfiniteStamina,
            HotkeysService.GetValueAsString(SectionEnum.Player, PlayerEnum.InfiniteStamina),
            _playerService.InfiniteStamina)
        {
            Checked = _playerService.InfiniteStamina
        };
        infiniteStamina.CheckboxChanged += (_, _) =>
        {
            _playerService.InfiniteStamina = infiniteStamina.Checked;
            Notify.CheckboxMessage("Infinite Stamina", infiniteStamina.Checked);
        };
        _playerService.InfiniteStaminaChanged += state =>
        {
            infiniteStamina.Checked = state;
            Notify.CheckboxMessage("Infinite Stamina", state);
        };
        Add(infiniteStamina);
    }
}