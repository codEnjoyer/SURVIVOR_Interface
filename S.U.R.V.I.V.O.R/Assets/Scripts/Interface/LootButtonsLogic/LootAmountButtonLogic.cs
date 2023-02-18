using Model;
using UnityEngine;
using UnityEngine.UI;

public class LootAmountButtonLogic : MonoBehaviour
{
    [SerializeField] private int LootAmount;
    private Button button;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private InterfaceController interfaceController;
    [SerializeField] private GameObject LootAmountButtonsLayer;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        if (GlobalMapController.Instance.ChosenGroup.Location.Data.CheckFight())
            return;
        interfaceController.SetGroupLayerActive();
        for (int i = 0; i < LootAmount; i++)
        {
            foreach (var item in GlobalMapController.Instance.ChosenGroup.Loot())
            {
                if (item == null) continue;
                inventoryController.SelectedInventoryGrid = LocationInventory.Instance.LocationInventoryGrid;
                inventoryController.AddItemToInventory(item);
            }
        }

        GlobalMapController.Instance.ChosenGroup.IsLootAllowedOnThisTurn = false;
    }
}