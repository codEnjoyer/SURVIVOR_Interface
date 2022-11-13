using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerCharacteristicsPanel : MonoBehaviour
{
    private Character player;

    public Character Player
    {
        get => player;
        set
        {
            OnDestroy();
            player = value;
            OnEnable();
        }
    }

    [SerializeField] private Image Photo;
    [SerializeField] private Text food;
    [SerializeField] private Text water;
    [SerializeField] private Text energy;
    [SerializeField] private GameObject healthProgressBar;

    public void Start()
    {
        Photo.sprite = Player.sprite;
    }

    private void OnFoodChanged(int value) => food.text = value.ToString();

    private void OnWaterChanged(int value) => water.text = value.ToString();

    private void OnEnergyChanged(int value) => energy.text = value.ToString();

    public void OnHealthChanged()
    {
    }

    private void OnEnable()
    {
        if(player is null) return;
        player.body.EnergyChange += OnEnergyChanged;
        player.body.WaterChange += OnWaterChanged;
        player.body.HungerChange += OnFoodChanged;
    }

    private void OnDestroy()
    {
        if(player is null) return;
        player.body.EnergyChange -= OnEnergyChanged;
        player.body.WaterChange -= OnWaterChanged;
        player.body.HungerChange -= OnFoodChanged;
    }
}