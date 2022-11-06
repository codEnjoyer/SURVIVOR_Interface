using System;
using UnityEngine;

public class Gun: Weapon
{
    [SerializeField] private GunData data;
    [SerializeField] private Magazine currentMagazine;
    [SerializeField] private Caliber chamber;
    [SerializeField] private GunModule gunModule;
    /* 
     * После патронника в реализации абстракных классов будут идти слоты для GunModule.
     * У каждого оружия будет свой допустимый набор возможных GunModules
     */

    public Shoot GiveShoot()
    {
        //TODO реализовать метод который возвращает служебный класс выстрела
        throw new NotImplementedException();
    }
}

