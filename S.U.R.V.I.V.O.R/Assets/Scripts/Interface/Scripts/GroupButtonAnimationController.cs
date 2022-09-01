using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupButtonAnimationController : MonoBehaviour
{
    private Animator Animator;

    private bool IsBarOpen;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        Animator = GetComponent<Animator>();
    }

    private void OnClick()
    {
        if(!IsBarOpen)
            Animator.Play("GroupBarOpen");
        else
        {
            Animator.Play("GroupBarClose");
        }

        IsBarOpen = !IsBarOpen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
