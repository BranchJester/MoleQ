using System;

namespace MoleQ.Exceptions;

public class BlipNotFoundException : Exception
{
    public BlipNotFoundException() : base("You need to place a waypoint on the map.")
    {
    }

    public BlipNotFoundException(string message) : base(message)
    {
    }
}