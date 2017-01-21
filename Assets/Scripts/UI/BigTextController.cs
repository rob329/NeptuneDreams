using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigTextController : MonoBehaviour
{
    Text uiText;
    Animator animator;

    // Use this for initialization
    void Start()
    {
        uiText = GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DebugText());
        }
    }

    private IEnumerator DebugText()
    {
        yield return ShowText("I wanna be the very best");
        yield return ShowText("Like noone ever was");
    }

    public IEnumerator ShowText(string text)
    {
        uiText.text = text;
        animator.SetTrigger("ShowText");
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "Invisible")
            {
                break;
            }
        }
        yield return null;
    }
}
