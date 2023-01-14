
using System;
using System.Linq;
using UnityEngine;

public class PlayerInfoLayersActivator : MonoBehaviour
{
    [SerializeField] private PlayerLayerLogic firstPlayerLayer;

    public void Awake()
    {
        firstPlayerLayer.Init(Game.Instance.ChosenGroup.CurrentGroupMembers.First());
    }
}