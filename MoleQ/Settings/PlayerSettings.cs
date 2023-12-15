using System;
using System.Collections.Generic;
using MoleQ.Enums;
using MoleQ.Interfaces.Player;

namespace MoleQ.Settings;

public class PlayerSettings : IServiceSettings
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

    public void ApplyToServices(IDictionary<Type, object> services)
    {
        if (services.TryGetValue(typeof(IPlayerService), out var playerService))
        {
            ((IPlayerService)playerService).Invincible = Invincible;
            ((IPlayerService)playerService).SuperJump = SuperJump;
            ((IPlayerService)playerService).LockWantedLevel = LockWantedLevel;
            ((IPlayerService)playerService).MaxWantedLevel = MaxWantedLevel;
            ((IPlayerService)playerService).InfiniteStamina = InfiniteStamina;
            ((IPlayerService)playerService).WantedLevel = WantedLevel;
            ((IPlayerService)playerService).InfiniteBreath = InfiniteBreath;
            ((IPlayerService)playerService).InfiniteSpecialAbility = InfiniteSpecialAbility;
        }

        if (services.TryGetValue(typeof(ISuperPunchService), out var superPunchService))
            ((ISuperPunchService)superPunchService).SuperPunch = SuperPunch;

        if (services.TryGetValue(typeof(ISuperRunService), out var superRunService))
            ((ISuperRunService)superRunService).SuperRun = SuperRun;
    }

    public void ExtractFromServices(IDictionary<Type, object> services)
    {
        if (services.TryGetValue(typeof(IPlayerService), out var playerService))
        {
            Invincible = ((IPlayerService)playerService).Invincible;
            SuperJump = ((IPlayerService)playerService).SuperJump;
            LockWantedLevel = ((IPlayerService)playerService).LockWantedLevel;
            MaxWantedLevel = ((IPlayerService)playerService).MaxWantedLevel;
            InfiniteStamina = ((IPlayerService)playerService).InfiniteStamina;
            WantedLevel = ((IPlayerService)playerService).WantedLevel;
            InfiniteBreath = ((IPlayerService)playerService).InfiniteBreath;
            InfiniteSpecialAbility = ((IPlayerService)playerService).InfiniteSpecialAbility;
        }

        if (services.TryGetValue(typeof(ISuperPunchService), out var superPunchService))
            SuperPunch = ((ISuperPunchService)superPunchService).SuperPunch;

        if (services.TryGetValue(typeof(ISuperRunService), out var superRunService))
            SuperRun = ((ISuperRunService)superRunService).SuperRun;
    }
}