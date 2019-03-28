using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuHoverOverButtons : MonoBehaviour
{
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GodIHateThisCode())
        {
            MouseOver();
        }
        else
        {
            MouseNotOver();
        }
    }

    private bool GodIHateThisCode()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    void MouseOver()
    {
        animator.SetBool("hovered", true);
        Debug.Log("over");
    }

    void MouseNotOver()
    {
        animator.SetBool("hovered", false);
        Debug.Log("gone");
    }
}
