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
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                hit.collider.attachedRigidbody.AddForce(Vector2.up);
            }
        }
    }


    public void MouseOver()
    {
        animator.SetBool("hovered", true);
        Debug.Log("over");
    }

    public void MouseNotOver()
    {
        animator.SetBool("hovered", false);
        Debug.Log("gone");
    }

    void OnMouseEnter()
    {
        Debug.Log("Test");
    }
}
