using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharactersPlateLayerLogic : MonoBehaviour
{
    [SerializeField] private PlayerCharacteristicsPanel FirstPlayerPanel;
    [SerializeField] private PlayerCharacteristicsPanel SecondPlayerPanel;
    [SerializeField] private PlayerCharacteristicsPanel ThirdPlayerPanel;
    [SerializeField] private PlayerCharacteristicsPanel FourthPlayerPanel;
    // Start is called before the first frame update
    public void Awake()
    {
        var playersArray = Game.Instance.ChosenGroup.CurrentGroupMembers.ToArray();
        FirstPlayerPanel.Player = playersArray[0];
        //SecondPlayerPanel.Player = playersArray[1];
        //ThirdPlayerPanel.Player = playersArray[2];
        //FourthPlayerPanel.Player = playersArray[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
