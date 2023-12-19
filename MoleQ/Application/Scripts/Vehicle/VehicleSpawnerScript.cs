using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTA;
using MoleQ.Application.Mappers;
using MoleQ.Application.ServiceInjector;
using MoleQ.Constants;
using MoleQ.Core.Application.Interfaces.Vehicle;
using MoleQ.Core.Domain.Enums;
using MoleQ.Core.Domain.Interfaces;
using MoleQ.Core.Domain.Settings;
using MoleQ.Infrastructure.Repositories;

namespace MoleQ.Application.Scripts.Vehicle;

public class VehicleSpawnerScript : BaseScript
{
    private readonly IStorageRepository _storageRepository;
    private readonly IVehicleSpawnerService _vehicleSpawnerService;

    public VehicleSpawnerScript()
    {
        _vehicleSpawnerService = Injector.VehicleSpawnerService;
        _storageRepository = new StorageRepository($"{Path.Settings}/VehicleSpawner.json");
        _vehicleSpawnerService.SpawnVehicleActivated += OnVehicleSpawned;
        KeyDown += OnKeyDown;
    }

    protected override void SaveSettings()
    {
        var settings = ServiceSettingsMapper.ExtractSettings<VehicleSpawnerSettings>(new Dictionary<Type, object>
        {
            { typeof(IVehicleSpawnerService), _vehicleSpawnerService }
        });
        _storageRepository.SaveSettings(settings);
    }

    protected override void LoadSettings()
    {
        var settings = _storageRepository.LoadSettings<VehicleSpawnerSettings>();
        var services = new Dictionary<Type, object>
        {
            { typeof(IVehicleSpawnerService), _vehicleSpawnerService }
        };
        ServiceSettingsMapper.ApplySettings(settings, services);
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        var warpInSpawnedKey = HotkeysService.GetValue(SectionEnum.VehicleSpawner, VehicleSpawnerEnum.WarpInSpawned);
        if (IsKeyPressed(warpInSpawnedKey))
            _vehicleSpawnerService.WarpInSpawned = !_vehicleSpawnerService.WarpInSpawned;

        var enginesRunningKey = HotkeysService.GetValue(SectionEnum.VehicleSpawner, VehicleSpawnerEnum.EnginesRunning);
        if (IsKeyPressed(enginesRunningKey))
            _vehicleSpawnerService.EnginesRunning = !_vehicleSpawnerService.EnginesRunning;
    }

    private void OnVehicleSpawned(VehicleHash vehicleHash)
    {
        SpawnVehicle(vehicleHash);
    }

    private void SpawnVehicle(VehicleHash vehicleHash)
    {
        var vehicle = World.CreateVehicle(vehicleHash,
            Game.Player.Character.Position + Game.Player.Character.ForwardVector * 5.0f,
            Game.Player.Character.Heading * -90.0f);
        vehicle.PlaceOnGround();
        vehicle.IsPersistent = true;

        if (_vehicleSpawnerService.WarpInSpawned)
        {
            vehicle.Heading = Game.Player.Character.Heading;
            Game.Player.Character.SetIntoVehicle(vehicle, VehicleSeat.Driver);
        }

        if (_vehicleSpawnerService.EnginesRunning) vehicle.IsEngineRunning = true;
    }
}