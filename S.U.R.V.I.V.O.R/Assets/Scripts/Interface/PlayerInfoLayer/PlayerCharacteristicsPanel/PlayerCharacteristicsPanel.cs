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
        food.text = player.body.Hunger.ToString();
        if (foodProgressBar != null)
            foodProgressBar.Init(player.body.Hunger);
        water.text = player.body.Water.ToString();
        if (waterProgressBar != null)
            waterProgressBar.Init(player.body.Water);
        energy.text = player.body.Energy.ToString();
        if (energyProgressBar != null)
            energyProgressBar.Init(player.body.Energy);
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