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
            Unsubscribe();
            player = value;
            Subscribe();
            Init();
        }
    }

    [SerializeField] private Image Photo;
    [SerializeField] private Text food;
    [SerializeField] private SegmentProgressBar foodProgressBar;
    [SerializeField] private Text water;
    [SerializeField] private SegmentProgressBar waterProgressBar;
    [SerializeField] private Text energy;
    [SerializeField] private SegmentProgressBar energyProgressBar;
    [SerializeField] private Text name;
    [SerializeField] private GameObject healthProgressBar;

    public void Init()
    {
        name.text = $"{Player.FirstName} {Player.Surname}";
        if (Photo != null)
            Photo.sprite = Player.Sprite;
        food.text = player.body.Hunger.ToString();
        if (foodProgressBar != null)
            foodProgressBar.Init();
        water.text = player.body.Water.ToString();
        if (waterProgressBar != null)
            waterProgressBar.Init();
        energy.text = player.body.Energy.ToString();
        if (energyProgressBar != null)
            energyProgressBar.Init();
    }

    private void OnFoodChanged(int value)
    {
        food.text = value.ToString();
        if (foodProgressBar != null)
            foodProgressBar.SetValue(value);
    }

    private void OnWaterChanged(int value)
    {
        water.text = value.ToString();
        if (waterProgressBar != null)
            waterProgressBar.SetValue(value);
    }

    private void OnEnergyChanged(int value)
    {
        energy.text = value.ToString();
        if (energyProgressBar != null)
            energyProgressBar.SetValue(value);
    }

    public void OnHealthChanged()
    {
    }

    private void Subscribe()
    {
        if(player is null) return;
        player.body.EnergyChange += OnEnergyChanged;
        player.body.WaterChange += OnWaterChanged;
        player.body.HungerChange += OnFoodChanged;
    }

    private void Unsubscribe()
    {
        if(player is null) return;
        player.body.EnergyChange -= OnEnergyChanged;
        player.body.WaterChange -= OnWaterChanged;
        player.body.HungerChange -= OnFoodChanged;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}