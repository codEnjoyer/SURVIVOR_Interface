using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LootButtonScript : MonoBehaviour
{
    private Button button;
    [SerializeField]
    private Group playerGroup;
    [SerializeField]
    private InventoryController inventoryController;
    [SerializeField]
    private InterfaceController interfaceController;
    [SerializeField]
    private ItemGrid LoactionItemGrid;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnClick()
    {
        interfaceController.SetGroupLayerActive();
        inventoryController.SelectedItemGrid = LoactionItemGrid;
        inventoryController.AddItemToInventory(playerGroup.location.GetLoot());
    }
}
