using System.Collections.Generic;


public interface IGun: IWeapon
{
    public bool IsFirstGun { get; }
    public GunData Data { get; }
    public ICollection<GunModule> GunModules { get; }
    
    public Magazine Reload(Magazine magazine);
}