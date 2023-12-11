using GTA;
using MoleQ.Interfaces.Weapon;
using MoleQ.ServiceInjector;

namespace MoleQ.StateHandlers;

/// <summary>
///     Used to update the state of the weapon service layer.
/// </summary>
public class WeaponStateHandler : IStateHandler
{
    private readonly IWeaponService _weaponService = Injector.WeaponService;

    public void UpdateState()
    {
        UpdateCurrentWeapon();
    }

    private void UpdateCurrentWeapon()
    {
        if (_weaponService.CurrentWeapon != Game.Player.Character.Weapons.Current)
            _weaponService.CurrentWeapon = Game.Player.Character.Weapons.Current;
    }
}