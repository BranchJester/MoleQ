using System;
using System.Collections.Generic;
using MoleQ.Core.Application.Interfaces.Player;
using MoleQ.Core.Domain.Enums;

namespace MoleQ.Core.Domain.Settings;

public class PlayerSettings : IServiceSettings
{
    public int WantedLevel { get; set; }
    public bool LockWantedLevel { set; get; }
    public int MaxWantedLevel { get; set; }
    public bool Invincible { get; set; }
    public bool InfiniteStamina { get; set; }
    public bool InfiniteBreath { get; set; }
    public bool InfiniteSpecialAbility { get; set; }
    public bool SuperJump { get; set; }
    public PlayerSuperSpeedEnum SuperRun { get; set; }
    public bool SuperPunch { get; set; }

    public void ApplyToServices(IDictionary<Type, object> services)
    {
        if (services.TryGetValue(typeof(IPlayerService), out var playerService))
        {
            ((IPlayerService)playerService).WantedLevel = WantedLevel;
            ((IPlayerService)playerService).LockWantedLevel = LockWantedLevel;
            ((IPlayerService)playerService).MaxWantedLevel = MaxWantedLevel;
            ((IPlayerService)playerService).Invincible = Invincible;
            ((IPlayerService)playerService).InfiniteStamina = InfiniteStamina;
            ((IPlayerService)playerService).InfiniteBreath = InfiniteBreath;
            ((IPlayerService)playerService).InfiniteSpecialAbility = InfiniteSpecialAbility;
            ((IPlayerService)playerService).SuperJump = SuperJump;
        }

        if (services.TryGetValue(typeof(ISuperRunService), out var superRunService))
            ((ISuperRunService)superRunService).SuperRun = SuperRun;

        if (services.TryGetValue(typeof(ISuperPunchService), out var superPunchService))
            ((ISuperPunchService)superPunchService).SuperPunch = SuperPunch;
    }

    public void ExtractFromServices(IDictionary<Type, object> services)
    {
        if (services.TryGetValue(typeof(IPlayerService), out var playerService))
        {
            WantedLevel = ((IPlayerService)playerService).WantedLevel;
            LockWantedLevel = ((IPlayerService)playerService).LockWantedLevel;
            MaxWantedLevel = ((IPlayerService)playerService).MaxWantedLevel;
            Invincible = ((IPlayerService)playerService).Invincible;
            InfiniteStamina = ((IPlayerService)playerService).InfiniteStamina;
            InfiniteBreath = ((IPlayerService)playerService).InfiniteBreath;
            InfiniteSpecialAbility = ((IPlayerService)playerService).InfiniteSpecialAbility;
            SuperJump = ((IPlayerService)playerService).SuperJump;
        }

        if (services.TryGetValue(typeof(ISuperRunService), out var superRunService))
            SuperRun = ((ISuperRunService)superRunService).SuperRun;

        if (services.TryGetValue(typeof(ISuperPunchService), out var superPunchService))
            SuperPunch = ((ISuperPunchService)superPunchService).SuperPunch;
    }
}