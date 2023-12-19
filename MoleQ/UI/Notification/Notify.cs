namespace MoleQ.Application.UI.Notification;

public abstract class Notify
{
    public static void Message(string message)
    {
        GTA.UI.Notification.Show($"~g~{message}");
    }

    public static void ErrorMessage(string message)
    {
        GTA.UI.Notification.Show($"~r~{message}");
    }

    public static void CheckboxMessage(string message, bool state)
    {
        if (state)
            GTA.UI.Notification.Show($"~g~{message} ~w~is now ~g~enabled");
        else
            GTA.UI.Notification.Show($"~g~{message} ~w~is now ~r~disabled");
    }
}