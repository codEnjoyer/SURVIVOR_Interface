using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decision
{
    public FightState State { get; set;}
    public GameObject Target { get; set;}

    public Decision (FightState state, GameObject target = null)
    {
        State = state;
        Target = target;
    }
}
