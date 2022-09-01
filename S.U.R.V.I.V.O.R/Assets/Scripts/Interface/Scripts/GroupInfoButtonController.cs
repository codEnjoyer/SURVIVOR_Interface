using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class GroupInfoButtonController : MonoBehaviour
{
    public GroupGameLogic ChosenGroup;

    private Text EnergyText;
    private Text WaterText;
    private Text HungerText;
    private Text HealthText;
    private Text GroupMemberCountText;
    private Text LocationText;

    public void Start()
    {
        EnergyText = transform.Find("EnergyImage").Find("EnergyText").GetComponent<Text>();
        WaterText = transform.Find("WaterImage").Find("WaterText").GetComponent<Text>();
        HealthText = transform.Find("HealthImage").Find("HealthText").GetComponent<Text>();
        HungerText = transform.Find("HungerImage").Find("HungerText").GetComponent<Text>();
        GroupMemberCountText = transform.Find("GroupMemberCountImage").Find("GroupMemberCountText").GetComponent<Text>();
        LocationText = transform.Find("CurrentLocationImage").Find("GroupCurrentLocationText").GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        EnergyText.text = ChosenGroup.GroupMembers.Average(x => x.Energy).ToString(CultureInfo.InvariantCulture);
        WaterText.text = ChosenGroup.GroupMembers.Average(x => x.Water).ToString(CultureInfo.InvariantCulture);
        HealthText.text = ChosenGroup.GroupMembers.Average(x => x.Hp).ToString(CultureInfo.InvariantCulture);
        HungerText.text = ChosenGroup.GroupMembers.Average(x => x.Satiety).ToString(CultureInfo.InvariantCulture);
        GroupMemberCountText.text = ChosenGroup.GroupMembers.Length.ToString();
        LocationText.text = ChosenGroup.GetComponentInParent<GroupMovementLogic>().CurrentNode.location.ToString();
    }

}
