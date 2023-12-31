﻿using System;

namespace MoleQ.Core.Application.Interfaces.Player;

public interface ISuperPunchService
{
    /// <summary>
    ///     Allows the player to super punch.
    /// </summary>
    bool SuperPunch { get; set; }

    event Action<bool> SuperPunchChanged;
}