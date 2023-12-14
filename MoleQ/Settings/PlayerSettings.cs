using MoleQ.Enums;

namespace MoleQ.Settings;

public class PlayerSettings
{
    public bool Invincible { get; set; }
    public bool SuperJump { get; set; }
    public int WantedLevel { get; set; }
    public bool LockWantedLevel { get; set; }
    public int MaxWantedLevel { get; set; }
    public bool InfiniteStamina { get; set; }
    public bool InfiniteBreath { get; set; }
    public bool InfiniteSpecialAbility { get; set; }
    public bool SuperPunch { get; set; }
    public PlayerSuperSpeedEnum SuperRun { get; set; }
}