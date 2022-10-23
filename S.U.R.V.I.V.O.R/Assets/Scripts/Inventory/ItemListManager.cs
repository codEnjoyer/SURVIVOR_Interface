using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemListManager : MonoBehaviour
{ 
    /*private InventoryController inventoryController;
    
    public List<GameObject> currentButtonList;
    public List<Item> currentItemList;
    
    [SerializeField] private GameObject ItemButtonPrefab;
    
    private Transform contentPanel;
    void Start()
    {
        contentPanel = this.transform;
        inventoryController = FindObjectOfType(typeof(InventoryController)) as InventoryController;
    }
    
    public void AddSelectedItemToList()
    {
        if (inventoryController.GetSelectedItem() != null)
        {
            var item = inventoryController.GetSelectedItem();
            currentItemList.Add(item);
            PopulateList(currentItemList);
            inventoryController.ResetSelectedItem();
        }
    }

    public void PopulateList(List<Item> passedItemlist)
    {
        if (currentButtonList.Count > 0)
        {
            for (int i = currentButtonList.Count - 1; i >= 0; i--)
            {
                RemoveButton(currentButtonList[i]);
            }
        }
        for (int j = 0; j < passedItemlist.Count; j++)
        {
            AddButton(passedItemlist[j]);
        }
    }

    public void AddButton(Item addItem)
    {
        var newButton = Instantiate(ItemButtonPrefab);
        newButton.transform.SetParent(contentPanel);
        newButton.GetComponent<RectTransform>().localScale = Vector3.one;
        newButton.GetComponent<ItemButtonScript>().SetUpButton(addItem);
        currentButtonList.Add(newButton);
    }
    
    public void RemoveButton(GameObject buttonObj)
    {
        // buttonObj.GetComponent<CanvasGroup>().alpha = 1f;
        currentButtonList.Remove(buttonObj);
        Destroy(buttonObj);
    }*/
}
