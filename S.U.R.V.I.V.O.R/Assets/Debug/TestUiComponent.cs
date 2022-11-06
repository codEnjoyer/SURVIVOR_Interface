using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestUiComponent : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Sprite sprite;

    public void Write(string str)
    {
        this.text.text = str;
    }
}
