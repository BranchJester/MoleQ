﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTA;
using MoleQ.Application.Mappers;
using MoleQ.Application.ServiceInjector;
using MoleQ.Application.UI.Notification;
using MoleQ.Constants;
using MoleQ.Core.Application.Interfaces.Vehicle;
using MoleQ.Core.Domain.Enums;
using MoleQ.Core.Domain.Exceptions;
using MoleQ.Core.Domain.Interfaces;
using MoleQ.Core.Domain.Settings;
using MoleQ.Infrastructure.Repositories;

namespace MoleQ.Application.Scripts.Vehicle;

public class VehicleBasicsScript : BaseScript
{
    private readonly IStorageRepository _storageRepository;
    private readonly IVehicleService _vehicleService;

    public VehicleBasicsScript()
    {
        _vehicleService = Injector.VehicleService;
        _storageRepository = new StorageRepository($"{Path.Settings}/Vehicle.json");
        _vehicleService.OnRepairVehicle += RepairVehicle;
        Tick += OnTick;
        KeyDown += OnKeyDown;
    }

    protected override void SaveSettings()
    {
        var settings = ServiceSettingsMapper.ExtractSettings<VehicleSettings>(new Dictionary<Type, object>
        {
            { typeof(IVehicleService), _vehicleService }
        });
        _storageRepository.SaveSettings(settings);
    }

    protected override void LoadSettings()
    {
        var settings = _storageRepository.LoadSettings<VehicleSettings>();
        var services = new Dictionary<Type, object>
        {
            { typeof(IVehicleService), _vehicleService }
        };
        ServiceSettingsMapper.ApplySettings(settings, services);
    }

    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        var repairVehicleKey = HotkeysService.GetValue(SectionEnum.Vehicle, VehicleEnum.RepairVehicle);
        if (IsKeyPressed(repairVehicleKey))
            try
            {
                _vehicleService.RepairVehicle();
            }
            catch (VehicleNotFoundException ex)
            {
                Notify.ErrorMessage(ex.Message);
            }

        var indestructibleVehicleKey = HotkeysService.GetValue(SectionEnum.Vehicle, VehicleEnum.Indestructible);
        if (IsKeyPressed(indestructibleVehicleKey))
            _vehicleService.Indestructible = !_vehicleService.Indestructible;
    }

    private void RepairVehicle()
    {
        var ped = Game.Player.Character;

        if (!ped.IsInVehicle()) throw new VehicleNotFoundException();

        var vehicle = ped.CurrentVehicle;
        vehicle.Repair();
    }

    private void OnTick(object sender, EventArgs e)
    {
        Indestructible();
    }

    private void Indestructible()
    {
        if (!Game.Player.Character.IsInVehicle()) return;

        var vehicle = Game.Player.Character.CurrentVehicle;
        vehicle.IsInvincible = _vehicleService.Indestructible;
        vehicle.CanBeVisiblyDamaged = false;

        if (vehicle.Health != vehicle.MaxHealth) vehicle.Repair();
    }
}