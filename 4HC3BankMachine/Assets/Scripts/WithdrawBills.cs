using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WithdrawBills : MonoBehaviour
{
    public TextMeshProUGUI amountToWithdrawText;
    public TextMeshProUGUI currentAmountSelectedText;

    public TextMeshProUGUI fiveQtyText;
    public TextMeshProUGUI tenQtyText;
    public TextMeshProUGUI twentyQtyText;
    public TextMeshProUGUI fiftyQtyText;

    public Button fiveQtyDown;
    public Button fiveQtyUp;
    public Button tenQtyDown;
    public Button tenQtyUp;
    public Button twentyQtyDown;
    public Button twentyQtyUp;
    public Button fiftyQtyDown;
    public Button fiftyQtyUp;

    private float amountToWithdraw;
    private float currentAmountSelected;
    private int fiveQty;
    private int tenQty;
    private int twentyQty;
    private int fiftyQty;
    
    private NavigationScript navigation;

    // Use this for initialization
    void Start()
    {
        // Setup Navigation Script
        navigation = GameObject.Find("EventSystem").GetComponent<NavigationScript>();

        // Setup variables
        fiveQty = 0;
        tenQty = 0;
        twentyQty = 0;
        fiftyQty = 0;
        currentAmountSelected = 0f;
        amountToWithdraw = 0f;

        // Initialize buttons
        fiveQtyDown.onClick.AddListener(DecreaseFiveQty);
        fiveQtyUp.onClick.AddListener(IncreaseFiveQty);
        tenQtyDown.onClick.AddListener(DecreaseTenQty);
        tenQtyUp.onClick.AddListener(IncreaseTenQty);
        twentyQtyDown.onClick.AddListener(DecreaseTwentyQty);
        twentyQtyUp.onClick.AddListener(IncreaseTwentyQty);
        fiftyQtyDown.onClick.AddListener(DecreaseFiftyQty);
        fiftyQtyUp.onClick.AddListener(IncreaseFiftyQty);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWithdrawAmount();
        UpdateBillQuantities();
        UpdateCurrentAmountSelected();
        SetButtonInteractability();
    }

    private void SetButtonInteractability()
    {
        // QUANTITY DOWN BUTTONS
        if (fiveQty == 0)
        {
            fiveQtyDown.interactable = false;
        }
        else
        {
            fiveQtyDown.interactable = true;
        }

        if (tenQty == 0)
        {
            tenQtyDown.interactable = false;
        }
        else
        {
            tenQtyDown.interactable = true;
        }

        if (twentyQty == 0)
        {
            twentyQtyDown.interactable = false;
        }
        else
        {
            twentyQtyDown.interactable = true;
        }

        if (fiftyQty == 0)
        {
            fiftyQtyDown.interactable = false;
        }
        else
        {
            fiftyQtyDown.interactable = true;
        }

        // QUANTITY UP BUTTONS
        if (currentAmountSelected + 5 > amountToWithdraw)
        {
            fiveQtyUp.interactable = false;
        }
        else
        {
            fiveQtyUp.interactable = true;
        }

        if (currentAmountSelected + 10 > amountToWithdraw)
        {
            tenQtyUp.interactable = false;
        }
        else
        {
            tenQtyUp.interactable = true;
        }

        if (currentAmountSelected + 20 > amountToWithdraw)
        {
            twentyQtyUp.interactable = false;
        }
        else
        {
            twentyQtyUp.interactable = true;
        }

        if (currentAmountSelected + 50 > amountToWithdraw)
        {
            fiftyQtyUp.interactable = false;
        }
        else
        {
            fiftyQtyUp.interactable = true;
        }
    }

    public void SetWithdrawAmount(float amount)
    {
        amountToWithdraw = amount;
        SetDefaultBillQtyValues();
    }

    public void ResetVariables()
    {
        fiveQty = 0;
        tenQty = 0;
        twentyQty = 0;
        fiftyQty = 0;
        currentAmountSelected = 0f;
    }

    // Called when user first reaches withdraw bills; sets up default values for bill quantities
    private void SetDefaultBillQtyValues()
    {
        ResetVariables();
        float amountLeft = amountToWithdraw;

        while (amountLeft != 0)
        {
            if (amountLeft >= 50)
            {
                fiftyQty++;
                amountLeft -= 50;
            }
            else if (amountLeft >= 20)
            {
                twentyQty++;
                amountLeft -= 20;
            }
            else if (amountLeft >= 10)
            {
                tenQty++;
                amountLeft -= 10;
            }
            else if (amountLeft >= 5)
            {
                fiveQty++;
                amountLeft -= 5;
            }
        }
    }

    // Only allow confirm to be clickable when currentAmountSelected equals amountToWithdraw
    private void SetupConfirmButton()
    {
        if (currentAmountSelected == amountToWithdraw)
        {
            navigation.EnableWithdrawBillConfirm();
        }
        else
        {
            navigation.DisableWithdrawBillConfirm();
        }
    }

    private void UpdateWithdrawAmount()
    {
        amountToWithdrawText.SetText("Amount to withdraw: $" + amountToWithdraw.ToString("0.00"));
    }

    // Update quantity values of bills
    private void UpdateBillQuantities()
    {
        fiveQtyText.SetText("Qty: " + fiveQty);
        tenQtyText.SetText("Qty: " + tenQty);
        twentyQtyText.SetText("Qty: " + twentyQty);
        fiftyQtyText.SetText("Qty: " + fiftyQty);
    }

    // Calculates and updates current amount selected
    private void UpdateCurrentAmountSelected()
    {
        // Calculate currentAmountSelected amount
        currentAmountSelected = 0f;
        currentAmountSelected += 5 * fiveQty;
        currentAmountSelected += 10 * tenQty;
        currentAmountSelected += 20 * twentyQty;
        currentAmountSelected += 50 * fiftyQty;

        // Update text field
        currentAmountSelectedText.SetText("Current amount selected: $" + currentAmountSelected.ToString("0.00"));

        if (currentAmountSelected != amountToWithdraw)
        {
            SetupConfirmButton();
            currentAmountSelectedText.faceColor = new Color32(255, 0, 0, 255);      // Set to text to red
        }
        else
        {
            SetupConfirmButton();
            currentAmountSelectedText.faceColor = new Color32(255, 255, 255, 255);  // Set to text to white
        }
    }

    private void DecreaseFiveQty()
    {
        if (fiveQty > 0)
        {
            fiveQty--;
        }
        else
        {
            fiveQty = 0;
        }
    }

    private void DecreaseTenQty()
    {
        if (tenQty > 0)
        {
            tenQty--;
        }
        else
        {
            tenQty = 0;
        }
    }

    private void DecreaseTwentyQty()
    {
        if (twentyQty > 0)
        {
            twentyQty--;
        }
        else
        {
            twentyQty = 0;
        }
    }

    private void DecreaseFiftyQty()
    {
        if (fiftyQty > 0)
        {
            fiftyQty--;
        }
        else
        {
            fiftyQty = 0;
        }
    }

    private void IncreaseFiveQty()
    {
        if (currentAmountSelected + 5f <= amountToWithdraw)
        {
            fiveQty++;
        }
        else
        {
            DisplayErrorMessage("Cannot add $5; currentAmountSelected will exceed amountToWithdraw!");
        }
    }

    private void IncreaseTenQty()
    {
        if (currentAmountSelected + 10f <= amountToWithdraw)
        {
            tenQty++;
        }
        else
        {
            DisplayErrorMessage("Cannot add $10; currentAmountSelected will exceed amountToWithdraw!");
        }
    }

    private void IncreaseTwentyQty()
    {
        if (currentAmountSelected + 20f <= amountToWithdraw)
        {
            twentyQty++;
        }
        else
        {
            DisplayErrorMessage("Cannot add $20; currentAmountSelected will exceed amountToWithdraw!");
        }
    }

    private void IncreaseFiftyQty()
    {
        if (currentAmountSelected + 50f <= amountToWithdraw)
        {
            fiftyQty++;
        }
        else
        {
            DisplayErrorMessage("Cannot add $50; currentAmountSelected will exceed amountToWithdraw!");
        }
    }

    private void DisplayErrorMessage(string error)
    {
        Debug.Log(error);
        EditorUtility.DisplayDialog("Error", error, "OK");
    }
}
