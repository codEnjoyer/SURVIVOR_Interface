using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ProgressBarScript : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float maxValue = 10;
    [SerializeField] private float value;
    private RectTransform valueRect;

    public void Init()
    {
        var rect = transform.Find("ValueRect");
        valueRect = rect.GetComponent<RectTransform>();
        rect.GetComponent<Image>().color = color;
        valueRect.localScale = new Vector3(2* value / maxValue, valueRect.localScale.y, valueRect.localScale.z);
        SetValue(maxValue);
    }

    public void SetValue(float newValue)
    {
        value = newValue;
        valueRect.localScale = new Vector3(2* value / maxValue, valueRect.localScale.y, valueRect.localScale.z);
    }
}
