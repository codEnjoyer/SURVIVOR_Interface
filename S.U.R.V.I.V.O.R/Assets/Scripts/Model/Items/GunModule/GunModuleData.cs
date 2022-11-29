﻿using UnityEngine;

[CreateAssetMenu(fileName = "New GunModuleData", menuName = "Data/GunModule Data", order = 50)]
public class GunModuleData: ScriptableObject
{
    [SerializeField] private float deltaRecoil;
    [SerializeField] private float deltaAccuracy;
    [SerializeField] private float deltaNoise;
    [SerializeField] private float deltaAverageDistance;
    [SerializeField] private float deltaDamage;
    [SerializeField] private float deltaErgonomics;
    [SerializeField] private GunModuleType moduleType;
    
    public float DeltaRecoil => deltaRecoil;
    public float DeltaAccuracy => deltaAccuracy;
    public float DeltaNoise => deltaNoise;
    public float DeltaAverageDistance => deltaAverageDistance;
    public float DeltaDamage => deltaDamage;
    public float DeltaErgonomics => deltaErgonomics;

    public GunModuleType ModuleType => moduleType;
}