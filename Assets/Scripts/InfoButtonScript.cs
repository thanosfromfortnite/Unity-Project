using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoButtonScript : MonoBehaviour, MainMenuButtons
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectButton()
    {
        
    }

    public void HoverButton()
    {
        gameObject.GetComponent<Animator>().SetBool("hovered", true);
    }

    public void LeaveButton()
    {
        gameObject.GetComponent<Animator>().SetBool("hovered", false);
    }
}
