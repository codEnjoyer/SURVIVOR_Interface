using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerCharacteristicsPanel : MonoBehaviour
{
    public Character Player { get; set; }
    [SerializeField] private Image Photo;
    [SerializeField] private Text food;
    [SerializeField] private Text water;
    [SerializeField] private Text energy;
    [SerializeField] private GameObject healthProgressBar;

    public void Start()
    {
        Photo.sprite = Player.sprite;
    }

    public Text Food => food;

    public Text Water => water;

    public Text Energy => energy;
    
    public void OnFoodChanged()
    {
        if (enabled)
            food.text = Player.Body.Hunger.ToString();
    }
    
    public void OnWaterChanged()
    {
        if (enabled)
            water.text = Player.Body.Water.ToString();
    }
    
    public void OnEnegryChanged()
    {
        if (enabled)
            energy.text = Player.Body.Energy.ToString();
    }
    
    public void OnHealthChanged()
    {
    }
    
}
