using UnityEngine;

public abstract class Selectable: MonoBehaviour
{
    public virtual void OnSelected(){}
    public virtual void OnDeselected(){}
}