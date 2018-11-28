using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmountScreen : MonoBehaviour
{
    private NavigationScript navigation;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI inputFieldTitleText;

    // Use this for initialization
    void Start()
    {
        // Setup Navigation Script
        navigation = GameObject.Find("EventSystem").GetComponent<NavigationScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the text on Amount screen depending on current functionMode
        int functionMode = navigation.functionMode;
        
        switch (functionMode)
        {
            // withdraw
            case 0:
                titleText.SetText("Enter an Amount to Withdraw");
                inputFieldTitleText.SetText("Amount to Withdraw:");
                break;
            // deposit
            case 1:
                titleText.SetText("Enter an Amount to Deposit");
                inputFieldTitleText.SetText("Amount to Deposit:");
                break;
            // eTransfer Send
            case 4:
                titleText.SetText("Enter an Amount to Send");
                inputFieldTitleText.SetText("Amount to Send:");
                break;
            // Pay Bills
            case 5:
                titleText.SetText("Enter an Amount to Pay");
                inputFieldTitleText.SetText("Amount to Pay:");
                break;
            // eTransfer Request
            case 6:
                titleText.SetText("Enter an Amount to Request");
                infoText.SetText("");
                inputFieldTitleText.SetText("Amount to Request:");
                break;
        }
    }
}