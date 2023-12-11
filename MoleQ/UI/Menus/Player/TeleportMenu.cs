using MoleQ.Enums;
using MoleQ.Exceptions;
using MoleQ.Interfaces.Player;
using MoleQ.UI.Items;
using MoleQ.UI.Menus.Abstract;
using MoleQ.UI.Notification;

namespace MoleQ.UI.Menus.Player;

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