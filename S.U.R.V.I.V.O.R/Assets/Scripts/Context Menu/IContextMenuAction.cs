using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Model;
using Model.Entities.Characters;
using UnityEngine;
using UnityEngine.UI;

public interface IContextMenuAction
{
    public string ButtonText { get; }
    
    public bool Extendable { get; }

    public void OnButtonClickAction<T>([CanBeNull] T value);

    public virtual IEnumerable GetValues()
    {
        var result = new List<Tuple<Character, string>>();
        foreach (var character in Game.Instance.ChosenGroup.CurrentGroupMembers)
        {
            result.Add(new Tuple<Character, string>(character, $"{character.FirstName} {character.Surname}"));
        }
        return result;
    }
}
