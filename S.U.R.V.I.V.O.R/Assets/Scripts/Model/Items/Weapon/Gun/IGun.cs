using System.Collections.Generic;
using System.Linq;


public interface IGun: IWeapon
{
    public bool IsFirstGun { get; }
    public GunData Data { get; }
    public IReadOnlyCollection<GunModule> GunModules { get; }

    public void AddGunModule(GunModule gunModule);
    
    public void RemoveGunModule(GunModule gunModule);

    public IReadOnlyCollection<SpecialCellType> AvailableGunModules { get; }
    public Magazine Reload(Magazine magazine);
}