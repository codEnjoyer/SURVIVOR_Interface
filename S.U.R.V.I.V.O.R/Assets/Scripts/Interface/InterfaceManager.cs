using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    public GameObject GroupButtonsLayer;
    public GameObject GroupInfoPanelLayer;
    public GameObject MainInfoPlateLayer;
    public GameObject CharactersPlateLayer;
    private GameObject activePlayerLayer;

    public void OpenPlayerInfoPanel(GameObject playerPlate)
    {
        activePlayerLayer.SetActive(false);
        GroupButtonsLayer.SetActive(false);
        playerPlate.SetActive(true);
        activePlayerLayer = playerPlate;
    }

    public void OpenGroupInfoPanel()
    {
        if (CharactersPlateLayer.activeInHierarchy)
        {
            CharactersPlateLayer.SetActive(false);
            if(activePlayerLayer != null)
                ClosePlayerInfoPanel();
        }
        GroupButtonsLayer.SetActive(false);
        GroupInfoPanelLayer.SetActive(true);
    }

    public void CloseGroupInfoPanel()
    {
        GroupInfoPanelLayer.SetActive(false);
        GroupButtonsLayer.SetActive(true);
    }

    public void ClosePlayerInfoPanel()
    {
        activePlayerLayer.SetActive(false);
        GroupButtonsLayer.SetActive(true);
        activePlayerLayer = null;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
