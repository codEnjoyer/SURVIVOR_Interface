using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Selectable: MonoBehaviour
{
    [SerializeField] private UnityEvent onSelected = new ();
    [SerializeField] private UnityEvent onDeselected = new ();

    public void Select() => onSelected?.Invoke();
    public void Deselect() => onDeselected?.Invoke();
}