using System;
using GTA;
using MoleQ.Constants;
using MoleQ.Enums;
using MoleQ.Interfaces.Player;
using MoleQ.Interfaces.Settings;
using MoleQ.ServiceInjector;
using MoleQ.Services.Settings;

namespace MoleQ.Scripts.Player;

public class SuperRunScript : BaseScript
{
    private readonly IPlayerService _playerService;
    private readonly IStorageService _storageService;
    private readonly ISuperRunService _superRunService;
    private float _maxSpeed;

    public SuperRunScript()
    {
        _playerService = Injector.PlayerService;
        _superRunService = Injector.SuperRunService;
        _storageService = new StorageService($"{Path.Settings}/SuperRun.json");
        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        SuperRun();
    }

    private void SuperRun()
    {
        switch (_superRunService.SuperRun)
        {
            case PlayerSuperSpeedEnum.Normal:
                SetRunSpeedMultiplier(1.00f);
                break;
            case PlayerSuperSpeedEnum.Fast:
                SetRunSpeedMultiplier(1.49f);
                break;
            case PlayerSuperSpeedEnum.Faster:
                Faster();
                break;
            case PlayerSuperSpeedEnum.Sonic:
                Sonic();
                break;
            case PlayerSuperSpeedEnum.Flash:
                Flash();
                break;
        }
    }

    private void Flash()
    {
        ApplySuperRun(100.0f);
        InvincibleWhileRunning();
        ApplyForceWhileRunning(20.0f);
    }

    private void ApplyForceWhileRunning(float forceMultiplier)
    {
        if (!Game.IsControlPressed(Control.Sprint)) return;

        var force = Game.Player.Character.ForwardVector * forceMultiplier * _maxSpeed;

        var nearbyEntity = World.GetNearbyEntities(Game.Player.Character.Position, 10.0f);
        foreach (var entity in nearbyEntity)
        {
            if (entity == Game.Player.Character) continue;
            if (Game.Player.Character.IsTouching(entity))
                entity.ApplyForce(force);
        }
    }

    private void Sonic()
    {
        ApplySuperRun(75.0f);
        InvincibleWhileRunning();
        ApplyForceWhileRunning(1.0f);
    }

    private void InvincibleWhileRunning()
    {
        if (_playerService.Invincible) return;
        if (!_playerService.Invincible && Game.IsControlPressed(Control.Sprint))
            Game.Player.Character.IsInvincible = true;
        else
            Game.Player.Character.IsInvincible = false;
    }

    private void Faster()
    {
        ApplySuperRun(49.0f);
    }

    private void ApplySuperRun(float maxSpeed)
    {
        // Ensure player is running
        if (!Game.IsControlPressed(Control.Sprint))
        {
            // Set if-condition to check if RideOnCars is activated.

            // Ensure the ragdoll goes back to normal when not sprinting.
            if (!Game.Player.Character.CanRagdoll)
                Game.Player.Character.CanRagdoll = true;
            return;
        }
        if (!Game.Player.Character.IsSprinting) return;

        // Set max speed
        _maxSpeed = maxSpeed;

        // Ensure ped is not ragdolling when sprinting, this prevents the character from tipping over when hitting
        // objects at high velocity.
        Game.Player.Character.CanRagdoll = false;

        // Set highest multiplier
        SetRunSpeedMultiplier(1.49f);

        // Max Speed
        Game.Player.Character.MaxSpeed = maxSpeed;

        // Only Apply force if player is not jumping and not climbing
        if (Game.IsControlPressed(Control.Jump) || Game.Player.Character.IsJumping ||
            Game.Player.Character.IsClimbing) return;

        // Increase forward velocity
        Game.Player.Character.ApplyForce(Game.Player.Character.ForwardVector * maxSpeed);

        // Increase downward velocity to keep player on the ground
        Game.Player.Character.ApplyForce(Game.Player.Character.UpVector * -11.0f);
    }


    private void SetRunSpeedMultiplier(float speedMultiplier)
    {
        Game.Player.SetRunSpeedMultThisFrame(speedMultiplier);
    }
}