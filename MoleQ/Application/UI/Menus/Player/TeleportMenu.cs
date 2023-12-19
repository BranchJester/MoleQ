using MoleQ.Application.UI.Items;
using MoleQ.Application.UI.Menus.Abstract;
using MoleQ.Application.UI.Notification;
using MoleQ.Core.Application.Interfaces.Player;
using MoleQ.Core.Domain.Enums;
using MoleQ.Core.Domain.Exceptions;

namespace MoleQ.Application.UI.Menus.Player;

public class TeleportMenu : BaseMenu
{
    private readonly ITeleportService _teleportService;

    public TeleportMenu(string menuName, ITeleportService teleportService) : base(menuName)
    {
        _teleportService = teleportService;
    }

    protected override void InitializeItems()
    {
        TeleportToMarker();
    }

    private void TeleportToMarker()
    {
        var teleportToMarker = new CustomNativeItem(TeleportEnum.TeleportToMarker,
            HotkeysService.GetValueAsString(SectionEnum.Teleport, TeleportEnum.TeleportToMarker));
        teleportToMarker.Activated += (_, _) =>
        {
            try
            {
                _teleportService.TeleportToBlip();
                Notify.Message("Teleported to Marker");
            }
            catch (BlipNotFoundException e)
            {
                Notify.ErrorMessage(e.Message);
            }
        };
        Add(teleportToMarker);
    }
}