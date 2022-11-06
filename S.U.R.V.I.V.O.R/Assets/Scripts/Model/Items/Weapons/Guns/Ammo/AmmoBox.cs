using System;
using System.Collections.Generic;
using UnityEngine;


public class AmmoBox : AmmoStorage
{
    private void Awake()
    {
        ammo = new Stack<Bullet>(Capacity);
        for (var i = 0; i < Capacity; i++)
            ammo.Push(CaliberType);
    }
}