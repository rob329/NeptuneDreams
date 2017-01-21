using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigTextController : MonoBehaviour
{
    Text uiText;
    Animator animator;

    public static BigTextController GetInstance()
    {
        return FindObjectOfType<BigTextController>();
    }

    void Start()
    {
        uiText = GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    public IEnumerator ShowText(string text)
    {
        uiText.text = text;
        animator.SetTrigger("ShowText");
        return WaitForText();
    }

    public IEnumerator WaitForText()
    {
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "ShowText"
        );
        yield return new WaitUntil(() =>
            animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Invisible"
        );
    }
}
