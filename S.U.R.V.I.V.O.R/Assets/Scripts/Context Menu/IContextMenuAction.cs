using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public interface IContextMenuAction
{
    public string ButtonText { get; }
    
    public bool Extendable { get; }

    public void OnButtonClickAction<T>([CanBeNull] T value);

    public IEnumerable GetValues();
}
