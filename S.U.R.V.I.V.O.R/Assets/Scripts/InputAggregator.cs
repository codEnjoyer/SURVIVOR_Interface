using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputAggregator : MonoBehaviour
{
    public Button turnEndButton;

    public static Action OnTurnEndEvent;

    public void Awake()
    {
        turnEndButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        OnTurnEndEvent();
    }
}
