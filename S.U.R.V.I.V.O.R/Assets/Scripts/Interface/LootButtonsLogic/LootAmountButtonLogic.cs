using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LootAmountButtonLogic : MonoBehaviour
{
    [SerializeField]
    private int LootAmount;
    private Button button;
    [SerializeField]
    private InventoryController inventoryController;
    [SerializeField]
    private InterfaceController interfaceController;
    [SerializeField]
    private ItemGrid LoactionItemGrid;
    [SerializeField]
    private Group playerGroup;
    [SerializeField]
    private GameObject LootAmountButtonsLayer;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        interfaceController.SetGroupLayerActive();
        for (int i = 0; i < LootAmount; i++)
        {
            inventoryController.SelectedItemGrid = LoactionItemGrid;
            inventoryController.AddItemToInventory(playerGroup.location.GetLoot());
        }
        LootAmountButtonsLayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
