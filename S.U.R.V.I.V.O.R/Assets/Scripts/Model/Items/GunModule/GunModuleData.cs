﻿using UnityEngine;

[CreateAssetMenu(fileName = "New GunModuleData", menuName = "Data/GunModule Data", order = 50)]
public class GunModuleData : ScriptableObject
{
    [SerializeField] private float deltaRecoil;
    [SerializeField] private float deltaSpreadSizeOnOptimalFireDistance;
    [SerializeField] private float deltaNoise;
    [SerializeField] private float deltaAverageDistance;
    [SerializeField] private float deltaDamage;
    [SerializeField] private float deltaErgonomics;
    [SerializeField] private GunModuleType moduleType;

    public GunModuleData(float deltaRecoil, float deltaSpreadSizeOnOptimalFireDistance, float deltaNoise, float deltaAverageDistance,
        float deltaDamage, float deltaErgonomics, GunModuleType moduleType)
    {
        this.deltaRecoil = deltaRecoil;
        this.deltaSpreadSizeOnOptimalFireDistance = deltaSpreadSizeOnOptimalFireDistance;
        this.deltaNoise = deltaNoise;
        this.deltaAverageDistance = deltaAverageDistance;
        this.deltaDamage = deltaDamage;
        this.deltaErgonomics = deltaErgonomics;
        this.moduleType = moduleType;
    }

    public float DeltaRecoil => deltaRecoil;
    public float DeltaSpreadSizeOnOptimalFireDistance => deltaSpreadSizeOnOptimalFireDistance;
    public float DeltaNoise => deltaNoise;
    public float DeltaAverageDistance => deltaAverageDistance;
    public float DeltaDamage => deltaDamage;
    public float DeltaErgonomics => deltaErgonomics;

    public GunModuleType ModuleType => moduleType;
}