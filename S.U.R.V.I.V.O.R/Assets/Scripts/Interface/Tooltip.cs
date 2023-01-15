using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    public static Tooltip Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Tooltip>();
                instance.Init();
            }

            return instance;
        }
    }

    private Text tooltipText;
    private RectTransform backgroundRectTransform;
    private Image backgroundImage;
    private RectTransform canvasRectTransform;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (instance == null)
        {
            instance = this;
            Init();
        }
    }

    private void Init()
    {
        tooltipText = transform.Find("Text").GetComponent<Text>();
        backgroundRectTransform = transform.Find("Background").GetComponent<RectTransform>();
        backgroundImage = backgroundRectTransform.GetComponent<Image>();
        canvasRectTransform = Game.Instance.MainCanvas.GetComponent<RectTransform>();
        HideTooltip();
    }

    public void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            Input.mousePosition,
            null,
            out localPoint
        );
        transform.localPosition = localPoint;
        Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;

        if (anchoredPosition.x < 0)
            anchoredPosition.x = 0;
        else if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;

        if (anchoredPosition.y < 0)
            anchoredPosition.y = 0;
        else if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;


        transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }

    private bool show;

    public void ShowTooltip(string tooltipString)
    {
        if (string.IsNullOrEmpty(tooltipString))
        {
            tooltipString = "Неизвестный предмет";
        }

        show = true;
        gameObject.SetActive(true);
        transform.SetAsLastSibling();
        StartCoroutine(SmoothAppearanceCoroutine());
        tooltipText.text = tooltipString;
        var textPaddingSize = 4f;
        var backgroundSize = new Vector2(
            tooltipText.preferredWidth + textPaddingSize * 2,
            tooltipText.preferredHeight + textPaddingSize * 2
        );
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    public void HideTooltip()
    {
        show = false;
        gameObject.SetActive(false);
    }


    IEnumerator SmoothAppearanceCoroutine()
    {
        var a = 0f;
        while (a < 1)
        {
            if (!show)
                yield break;
            
            a += 0.01f;
            tooltipText.color = new Color
            (
                tooltipText.color.r,
                tooltipText.color.g,
                tooltipText.color.b,
                a
            );

            backgroundImage.color = new Color
            (
                backgroundImage.color.r,
                backgroundImage.color.g,
                backgroundImage.color.b,
                a
            );
            yield return null;
        }
    }
}