﻿using System;
using UnityEngine;

[RequireComponent(typeof(BaseItem))]
public class Gun: MonoBehaviour
{
    [SerializeField] private GunData data;
    [SerializeField] private Magazine currentMagazine;
    [SerializeField] private SingleAmmo chamber;
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
