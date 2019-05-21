using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    private MainMenuButtons[] buttons;
    private int selectedButton = 0;
    private int previousButton = 0;

    // Start is called before the first frame update
    void Start()
    {
        buttons = new MainMenuButtons[2];
        buttons[0] = GameObject.Find("PlayButton").GetComponent<PlayButtonScript>();
        buttons[1] = GameObject.Find("InfoButton").GetComponent<InfoButtonScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")) {
            Scroll(true);
        }
        if (Input.GetKey("s"))
        {
            Scroll(false);
        }
    }

    private void Scroll(bool forward)
    {
        previousButton = selectedButton;
        if (forward)
        {
            selectedButton++;
            if (selectedButton >= buttons.Length)
            {
                selectedButton = 0;
            }
        }
        else
        {
            selectedButton--;
            if (selectedButton <= 0)
            {
                selectedButton = buttons.Length - 1;
            }
        }
        buttons[previousButton].LeaveButton();
        buttons[selectedButton].HoverButton();
        Debug.Log("Left" + previousButton + ", Selected " + selectedButton);
    }

}
