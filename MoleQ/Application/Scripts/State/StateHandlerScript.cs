using System;
using System.Collections.Generic;
using GTA;
using MoleQ.Application.StateHandlers;

namespace MoleQ.Application.Scripts.State;

public class StateHandlerScript : Script
{
    private readonly List<IStateHandler> _stateHandlers = new();

    public StateHandlerScript()
    {
        _stateHandlers.Add(new PlayerStateHandler());
        _stateHandlers.Add(new VehicleStateHandler());
        _stateHandlers.Add(new WeaponStateHandler());

        Tick += OnTick;
        Interval = 200; // Re-visit this if events starts to f up.
    }

    private void OnTick(object sender, EventArgs e)
    {
        foreach (var stateHandler in _stateHandlers)
        {
            stateHandler.UpdateState();
        }
    }
}