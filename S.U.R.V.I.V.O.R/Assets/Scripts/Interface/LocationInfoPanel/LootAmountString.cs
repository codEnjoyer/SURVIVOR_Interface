using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootAmountString : MonoBehaviour
{
    [SerializeField] private string lootTypeText;//патронов, медикаментов, материалов
    [SerializeField] private Text messageOfLootAmountText;
    [SerializeField] private Image backgroundImage;
    

    public void Redraw(float dropChance, List<Color> colorsToDraw)
    {
        if (dropChance == 0)
        {
            var color = colorsToDraw[0];
            messageOfLootAmountText.text = $"Нет {lootTypeText}";
            backgroundImage.gameObject.GetComponent<Image>().color = new Color(color.r,color.g,color.b,255);
        }
        else if (dropChance > 0 && dropChance <= 5)
        {
            var color = colorsToDraw[1];
            messageOfLootAmountText.text = $"Мало {lootTypeText}"; 
            backgroundImage.color = new Color(color.r,color.g,color.b,255);
        }
        else if (dropChance > 5 && dropChance <= 10)
        {
            var color = colorsToDraw[2];
            messageOfLootAmountText.text = $"Достаточно {lootTypeText}"; 
            backgroundImage.color = new Color(color.r,color.g,color.b,255);
        }
        else if (dropChance > 10 && dropChance <= 20)
        {
            var color = colorsToDraw[3];
            messageOfLootAmountText.text = $"Много {lootTypeText}"; 
            backgroundImage.color = new Color(color.r,color.g,color.b,255);
        }
        else
        {
            var color = colorsToDraw[4];
            messageOfLootAmountText.text = $"Очень много {lootTypeText}"; 
            backgroundImage.color = new Color(color.r,color.g,color.b,255);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
