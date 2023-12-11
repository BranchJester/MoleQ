﻿using GTA;
using MoleQ.Interfaces.Player;
using MoleQ.ServiceInjector;

namespace MoleQ.StateHandlers;

/// <summary>
///     Used to update the state of the player service layer.
/// </summary>
public class PlayerStateHandler : IStateHandler
{
    private readonly IPlayerService _playerService = Injector.PlayerService;

    public void UpdateState()
    {
        UpdateWantedLevel();
    }

    private void UpdateWantedLevel()
    {
        if (_playerService.WantedLevel != Game.Player.WantedLevel)
            _playerService.WantedLevel =
                _playerService.LockWantedLevel ? _playerService.MaxWantedLevel : Game.Player.WantedLevel;

        if (_playerService.Character != Game.Player.Character) _playerService.Character = Game.Player.Character;
    }
}