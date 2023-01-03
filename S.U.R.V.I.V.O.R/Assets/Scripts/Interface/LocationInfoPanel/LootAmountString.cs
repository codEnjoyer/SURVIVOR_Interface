using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootAmountString : MonoBehaviour
{
    [SerializeField] private string lootTypeText;//патронов, медикаментов, материалов
    [SerializeField] private Text messageOfLootAmountText;

    public void Redraw(int categoryItemsCount, int AllItemsCountWithZero)
    {
        var dropChance = (categoryItemsCount / AllItemsCountWithZero)*100;
        if (dropChance == 0)
        {
            messageOfLootAmountText.text = $"Нет {lootTypeText}"; 
        }
        else if (dropChance > 0 && dropChance <= 5)
        {
            messageOfLootAmountText.text = $"Мало {lootTypeText}"; 
        }
        else if (dropChance > 5 && dropChance <= 10)
        {
            messageOfLootAmountText.text = $"Достаточно {lootTypeText}"; 
        }
        else if (dropChance > 10 && dropChance <= 20)
        {
            messageOfLootAmountText.text = $"Много {lootTypeText}"; 
        }
        else
        {
            messageOfLootAmountText.text = $"Очень много {lootTypeText}"; 
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
