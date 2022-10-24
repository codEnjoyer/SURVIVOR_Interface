using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LootButtonScript : MonoBehaviour
{
    private Button button;
    [SerializeField]
    private GameObject LootAmountButtonsLayer;

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
        if(LootAmountButtonsLayer.activeInHierarchy)
            LootAmountButtonsLayer.SetActive(false);
        else LootAmountButtonsLayer.SetActive(true);
    }
}
