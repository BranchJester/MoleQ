using System;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using MoleQ.Application.ServiceInjector;
using MoleQ.Constants;
using MoleQ.Core.Application.Interfaces.Player;
using MoleQ.Core.Domain.Enums;
using MoleQ.Core.Domain.Interfaces;
using MoleQ.Infrastructure.Repositories;

namespace MoleQ.Application.Scripts.Player;

public class SuperPunchScript : BaseScript
{
    private readonly IStorageRepository _storageRepository;
    private readonly ISuperPunchService _superPunchService;

    public SuperPunchScript()
    {
        _superPunchService = Injector.SuperPunchService;
        _storageRepository = new StorageRepository($"{Path.Settings}/SuperPunch.json");
        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        var superPunch = HotkeysService.GetValue(SectionEnum.Player, PlayerEnum.SuperPunch);
        if (IsKeyPressed(superPunch))
            _superPunchService.SuperPunch = !_superPunchService.SuperPunch;
    }

    private void OnTick(object sender, EventArgs e)
    {
        SuperPunch();
    }

    private void SuperPunch()
    {
        if (!_superPunchService.SuperPunch) return;
        if (!Game.Player.Character.IsInMeleeCombat) return;

        var nearbyEntities = World.GetNearbyEntities(Game.Player.Character.Position, 10.0f)
            .Where(e => e != Game.Player.Character);
        var firstEntity = nearbyEntities.OrderBy(e => Vector3.Distance(Game.Player.Character.Position, e.Position))
            .FirstOrDefault(e =>
                e.HasBeenDamagedBy(Game.Player.Character) && e.HasBeenDamagedBy(WeaponHash.Unarmed));


        if (firstEntity == null) return;
        firstEntity.ApplyForce(Game.Player.Character.ForwardVector * 10000.0f);
    }
}