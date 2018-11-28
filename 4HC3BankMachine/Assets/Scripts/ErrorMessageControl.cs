using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ErrorMessageControl : MonoBehaviour {

    public Canvas popup;
    public Image background;
    private string errorMessage;
    public TextMeshProUGUI errorPopup;
    public Button confirm;

	// Use this for initialization
	void Start () {
        //set opacity of background to "darken" out other features
        Color temp = background.color;
        temp.a = 0.8f;
        background.color = temp;
        errorPopup.SetText("Error: " + errorMessage);

        //set confirm button
        confirm.onClick.AddListener(taskConfirm);
    }

    //change error message to input
    public void changeErrorMessageTo(string input)
    {
        errorMessage = input;
    }

    //close popup on confirm
    void taskConfirm()
    {
        popup.enabled = false;
    }
}
