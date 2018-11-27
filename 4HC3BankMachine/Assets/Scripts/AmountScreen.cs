using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmountScreen : MonoBehaviour
{
    private NavigationScript navigation;

    public TextMeshProUGUI enterAnAmountToText;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI amountToText;

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

        // NEED TO UDPDATE THIS
        switch (functionMode)
        {
            // withdraw
            case 0:
                {
                    enterAnAmountToText.SetText("Enter an Amount to Withdraw");
                    infoText.SetText("Current Balance: ");  // TODO: update with current balance!
                    amountToText.SetText("Amount to Withdraw");
                    break;
                }
            // deposit
            case 1:
                {
                    enterAnAmountToText.SetText("Enter an Amount to Deposit");
                    infoText.SetText("Current Balance: ");  // TODO: update with current balance!
                    amountToText.SetText("Amount to Deposit");
                    break;
                }
            // eTransfer
            case 3:
                {
                    enterAnAmountToText.SetText("Enter an Amount to Send");
                    infoText.SetText("Current Balance: \nRecipient: ");  // TODO: update with information!
                    amountToText.SetText("Amount to Send");
                    break;
                }
        }
    }
}
