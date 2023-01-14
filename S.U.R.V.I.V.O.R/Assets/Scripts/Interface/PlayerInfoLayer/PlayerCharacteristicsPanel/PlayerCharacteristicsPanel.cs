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
    [SerializeField] private Text water;
    [SerializeField] private Text energy;
    [SerializeField] private Text name;
    [SerializeField] private GameObject healthProgressBar;

    public void Init()
    {
        name.text = $"{Player.FirstName} {Player.Surname}";
        if (Photo != null)
            Photo.sprite = Player.Sprite;
        food.text = player.body.Hunger.ToString();
        water.text = player.body.Water.ToString();
        energy.text = player.body.Energy.ToString();
    }

    private void OnFoodChanged(int value) => food.text = value.ToString();

    private void OnWaterChanged(int value) => water.text = value.ToString();

    private void OnEnergyChanged(int value) => energy.text = value.ToString();

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