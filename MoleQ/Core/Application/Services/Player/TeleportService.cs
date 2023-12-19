using System;
using GTA;
using MoleQ.Core.Application.Interfaces.Player;

namespace MoleQ.Core.Application.Services.Player;

public class TeleportService : ITeleportService
{
    public event Action<BlipSprite> OnTeleportToBlip;

    public void TeleportToBlip(BlipSprite blipSprite = BlipSprite.Waypoint)
    {
        OnTeleportToBlip?.Invoke(blipSprite);
    }
}