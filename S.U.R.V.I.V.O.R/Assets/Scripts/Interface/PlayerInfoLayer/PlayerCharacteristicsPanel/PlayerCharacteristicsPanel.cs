using Model.Entities.Characters;
using UnityEngine;
using UnityEngine.Serialization;
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
    [FormerlySerializedAs("name")] [SerializeField] private Text characterName;
    [SerializeField] private GameObject healthProgressBar;

    public void Init()
    {
        characterName.text = $"{Player.FirstName} {Player.Surname}";
        if (Photo != null)
            Photo.sprite = Player.Sprite;
        food.text = player.ManBody.Hunger.ToString();
        if (foodProgressBar != null)
            foodProgressBar.Init(player.ManBody.Hunger);
        water.text = player.ManBody.Water.ToString();
        if (waterProgressBar != null)
            waterProgressBar.Init(player.ManBody.Water);
        energy.text = player.ManBody.Energy.ToString();
        if (energyProgressBar != null)
            energyProgressBar.Init(player.ManBody.Energy);
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
        player.ManBody.EnergyChange += OnEnergyChanged;
        player.ManBody.WaterChange += OnWaterChanged;
        player.ManBody.HungerChange += OnFoodChanged;
    }

    private void Unsubscribe()
    {
        if(player is null) return;
        player.ManBody.EnergyChange -= OnEnergyChanged;
        player.ManBody.WaterChange -= OnWaterChanged;
        player.ManBody.HungerChange -= OnFoodChanged;
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}