using System.Collections;
using System.Collections.Generic;
using Player;
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
        //if(playerGroup.location.Data.CheckFight())
            //return;
        interfaceController.SetGroupLayerActive();
        for (int i = 0; i < LootAmount; i++)
        {
            inventoryController.SelectedItemGrid = LocationManager.Instance.ItemGrid;
            inventoryController.AddItemToInventory(playerGroup.location.Data.GetLoot());
        }
    }
}
