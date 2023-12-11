using System;
using GTA;
using MoleQ.Interfaces.Player;

namespace MoleQ.Services.Player;

public class TeleportService : ITeleportService
{
    public event Action<BlipSprite> OnTeleportToBlip;

    public void TeleportToBlip(BlipSprite blipSprite = BlipSprite.Waypoint)
    {
        OnTeleportToBlip?.Invoke(blipSprite);
    }
}