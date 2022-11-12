using System.Collections.Generic;


public interface IGun: IWeapon
{
    public GunData Data { get; }
    public Caliber Caliber { get; }
    
    
    public IEnumerable<GunModule> GunModules { get; }
    
    public Magazine Reload(Magazine magazine);
}