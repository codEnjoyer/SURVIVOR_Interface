using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
    public static TurnController Instance { get; private set; }
    [SerializeField] private Button turnEndButton;
    private UnityEvent turnEnd;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Init();
        }
        else if (Instance == this)
            Destroy(gameObject);
    }

    private void Init()
    {
        turnEnd = new UnityEvent();
        turnEndButton.onClick.AddListener(turnEnd.Invoke);
    }

    public void AddListener(UnityAction action) => turnEnd.AddListener(action);
}
