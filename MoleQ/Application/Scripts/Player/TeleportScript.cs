﻿using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using MoleQ.Application.ServiceInjector;
using MoleQ.Application.UI.Notification;
using MoleQ.Core.Application.Interfaces.Player;
using MoleQ.Core.Domain.Enums;
using MoleQ.Core.Domain.Exceptions;

namespace MoleQ.Application.Scripts.Player;

public class TeleportScript : BaseScript
{
    private readonly ITeleportService _teleportService;

    public TeleportScript()
    {
        _teleportService = Injector.TeleportService;
        _teleportService.OnTeleportToBlip += TeleportToBlip;

        KeyDown += OnKeyDown;
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        try
        {
            var teleportToBlipKey = HotkeysService.GetValue(SectionEnum.Teleport, TeleportEnum.TeleportToMarker);
            if (IsKeyPressed(teleportToBlipKey))
                TeleportToBlip(BlipSprite.Waypoint);
        }
        catch (BlipNotFoundException ex)
        {
            Notify.ErrorMessage(ex.Message);
        }
    }

    private void TeleportToBlip(BlipSprite blipSprite)
    {
        Entity entity = Game.Player.Character;

        if (entity is Ped ped)
            if (ped.IsInVehicle())
                entity = Game.Player.Character.CurrentVehicle;

        var blip = World
            .GetAllBlips()
            .FirstOrDefault(b => b.Sprite == blipSprite);

        if (blip == null) throw new BlipNotFoundException();

        var blipPos = blip.Position;
        var zCoords = 0.0f;

        entity.Position = new Vector3(blipPos.X, blipPos.Y, zCoords);
        Wait(200);

        if (entity.IsInWater) return;

        var antiFreezeCounter = 0;

        while (entity.Position.Z < World.GetGroundHeight(entity.Position) ||
               World.GetGroundHeight(entity.Position) == 0)
        {
            antiFreezeCounter++;
            entity.Position = new Vector3(entity.Position.X, entity.Position.Y, zCoords += 3.0f);

            if (antiFreezeCounter <= 20) continue;

            Yield();
            antiFreezeCounter = 0;
        }
    }
}