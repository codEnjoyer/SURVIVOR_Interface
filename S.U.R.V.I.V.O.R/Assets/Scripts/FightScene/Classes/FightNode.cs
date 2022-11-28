using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightNode : MonoBehaviour
{
    public List<FightNode> Neighbours = new List<FightNode>();
    public string Index;
    public NodeType Type;
}
