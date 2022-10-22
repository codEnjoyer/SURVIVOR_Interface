using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputAggregator : MonoBehaviour
{
    [SerializeField] private Button turnEndButton;

    public static event Action OnTurnEndEvent;

    public void Awake()
    {
        turnEndButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        OnTurnEndEvent?.Invoke();
    }
}
