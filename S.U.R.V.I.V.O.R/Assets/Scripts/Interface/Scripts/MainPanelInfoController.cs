using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Model;
using UnityEngine;

public class MainPanelInfoController : MonoBehaviour
{
    public PlayerInfoController PlayerInfoController;

    private GroupInfoButtonController ChosenGroupInfoButtonController;
    private GameObject GroupMembersPanel;

    private Group GroupToFirstGroupPanel;
    private Group ChosenGroup;

    private void Awake()
    {
        ChosenGroup = GroupToFirstGroupPanel;// Это будет нужно, чтобы панель игроков могла показывать не только одну группу, а переключаться между ними, а пока можно так.
        GroupToFirstGroupPanel = PlayerInfoController.MainGroup;
        ChosenGroupInfoButtonController = transform.Find("GroupInfoButton").GetComponent<GroupInfoButtonController>();
        ChosenGroupInfoButtonController.ChosenGroup = GroupToFirstGroupPanel;
    }
}
