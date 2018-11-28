using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Accounts : MonoBehaviour
{
    // Error message screen
    public Canvas errorScreen;

    // "Current Balance" texts
    public TextMeshProUGUI accountSummaryText;
    public TextMeshProUGUI checkBalanceCurrentBalanceText;
    public TextMeshProUGUI amountInfoText;
    public TextMeshProUGUI receiptInfoText;
    public TextMeshProUGUI withdrawCurrentBalanceText;

    // Verification
    public TextMeshProUGUI verificationTitleText;
    public TextMeshProUGUI verificationTitle1Text;
    public TextMeshProUGUI verificationTitle2Text;
    public TextMeshProUGUI verificationAmountText;

    // Withdraw
    public Button withdraw20;
    public Button withdraw40;
    public Button withdraw60;
    public Button withdraw80;
    public Button withdraw100;
    public Button withdrawCustom;

    // Transfer Money
    public TMP_Dropdown fromDropdownTransfer;
    public TMP_Dropdown toDropdownTransfer;

    // Accounts
    private Account chequing;
    private Account savings;
    private Account taxFreeSavings;
    private Account basicAccount;

    // Recipients
    private Recipient bob;
    private Recipient andrea;
    private Recipient frank;

    // Payee
    private Payee mcMaster;
    private Payee electricCompany;
    private Payee canadianTaxRevenue;

    // Currently selected account
    private Account selectedAccount;
    private Account toAccount;

    private Recipient selectedRecipient;

    // Navigation
    private NavigationScript navigation;

    // Pin controls
    private PinControl amountPinControl;
    private float verificationAmount;

    // Use this for initialization
    void Start()
    {
        navigation = GameObject.Find("EventSystem").GetComponent<NavigationScript>();
        amountPinControl = GameObject.Find("Deposit/Withdraw/eTransfer_Amount").GetComponent<PinControl>();

        // Initialize all 4 accounts
        chequing = new Account("Chequing", 2200);
        savings = new Account("Savings", 10450);
        taxFreeSavings = new Account("Tax-Free Savings", 4300);
        basicAccount = new Account("Basic Account", 10);

        // Initialize all 3 recipients
        bob = new Recipient("Bob Jones");
        andrea = new Recipient("Andrea He");
        frank = new Recipient("Frank Doe");

        // Initialize all 3 payees
        mcMaster = new Payee("McMaster University");
        electricCompany = new Payee("Electric Company");
        canadianTaxRevenue = new Payee("Canadian Tax Revenue Agency");

        // Setup transfer money dropdown updates
        fromDropdownTransfer.onValueChanged.AddListener(FromDropdownTransferOnValueChanged);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateTransferMoneyFromDropdown()
    {
        // Clear options
        fromDropdownTransfer.ClearOptions();

        // Get accounts with balances
        string account1 = chequing.GetName() + " | " + ToMoneyFormat(chequing.GetBalance());
        string account2 = savings.GetName() + " | " + ToMoneyFormat(savings.GetBalance());
        string account3 = taxFreeSavings.GetName() + " | " + ToMoneyFormat(taxFreeSavings.GetBalance());
        string account4 = basicAccount.GetName() + " | " + ToMoneyFormat(basicAccount.GetBalance());

        // Create list of accounts
        List<string> accounts = new List<string>();
        accounts.Add(account1);
        accounts.Add(account2);
        accounts.Add(account3);
        accounts.Add(account4);

        // Add accounts to options
        fromDropdownTransfer.AddOptions(accounts);

        StartCoroutine(UpdateTransferMoneyToDropdown());
    }

    private void FromDropdownTransferOnValueChanged(int arg0)
    {
        StartCoroutine(UpdateTransferMoneyToDropdown());
    }

    public void UpdateValidationScreenText(GameObject objectWithPinControl)
    {
        PinControl pinControl = objectWithPinControl.GetComponent<PinControl>();
        UpdateValidationScreenText(float.Parse(pinControl.input));
    }

    public void UpdateValidationScreenText(float amount)
    {
        verificationAmount = amount;

        if (!ValidateAmount())
        {
            return;
        }

        switch (navigation.functionMode)
        {
            // withdraw
            case 0:
                verificationTitleText.SetText("Withdraw Verification");
                verificationTitle1Text.SetText("Withdraw");
                verificationAmountText.SetText(ToMoneyFormat(verificationAmount));
                verificationTitle2Text.SetText("from " + selectedAccount.GetName() + "?");
                break;
            // deposit
            case 1:
                verificationTitleText.SetText("Deposit Verification");
                verificationTitle1Text.SetText("Deposit");
                verificationAmountText.SetText(ToMoneyFormat(verificationAmount));
                verificationTitle2Text.SetText("to " + selectedAccount.GetName() + "?");
                break;
            // Transfer b/w accounts
            case 3:
                verificationTitleText.SetText("Transfer Verification");
                verificationTitle1Text.SetText("Transfer");
                string fromAccount = fromDropdownTransfer.options[fromDropdownTransfer.value].text;
                string toAccountStr = toDropdownTransfer.options[toDropdownTransfer.value].text;
                fromAccount = fromAccount.Split('|')[0];
                fromAccount = fromAccount.Substring(0, fromAccount.Length - 1);
                toAccountStr = toAccountStr.Split('|')[0];
                toAccountStr = toAccountStr.Substring(0, toAccountStr.Length - 1);
                verificationTitle2Text.SetText("from " + fromAccount + " to " + toAccountStr + "?");
                verificationAmountText.SetText(ToMoneyFormat(verificationAmount));
                Debug.Log("fromAccount = " + fromAccount + ", toAccount = " + toAccountStr);
                selectedAccount = GetAccountFromName(fromAccount);
                toAccount = GetAccountFromName(toAccountStr);
                break;
            // eTransfer Pay
            case 4:
                verificationTitleText.SetText("E-Transfer Verification");
                verificationTitle1Text.SetText("Pay");
                verificationAmountText.SetText(ToMoneyFormat(verificationAmount));
                verificationTitle2Text.SetText("from " + selectedAccount.GetName() + " to " + selectedRecipient.GetName() + "?");
                break;
            // Pay Bills
            case 5:
                verificationTitleText.SetText("Transfer Verification");
                verificationTitle1Text.SetText("Pay");
                string fromAccount2 = fromDropdownTransfer.options[fromDropdownTransfer.value].text;
                fromAccount2 = fromAccount2.Split('|')[0];
                fromAccount2 = fromAccount2.Substring(0, fromAccount2.Length - 1);
                selectedAccount = GetAccountFromName(fromAccount2);
                string payee = toDropdownTransfer.options[toDropdownTransfer.value].text;
                verificationTitle2Text.SetText("from " + fromAccount2 + " to " + payee + "?");
                verificationAmountText.SetText(ToMoneyFormat(verificationAmount));
                break;
            // eTransfer Request
            case 6:
                verificationTitleText.SetText("E-Transfer Verification");
                verificationTitle1Text.SetText("Request");
                verificationAmountText.SetText(ToMoneyFormat(verificationAmount));
                verificationTitle2Text.SetText("from " + selectedRecipient.GetName() + "?");
                break;
        }
    }

    // Throws error message if user is requesting to use an amount more than the account balance
    public bool ValidateAmount()
    {
        bool success = true;
        ErrorMessageControl errorMessage = errorScreen.GetComponent<ErrorMessageControl>();

        switch (navigation.functionMode)
        {
            // withdraw
            case 0:
                if (verificationAmount > selectedAccount.GetBalance())
                {
                    errorMessage.errorPopup.SetText("The balance of " +
                        selectedAccount.GetName() + " is too low to withdraw " + ToMoneyFormat(verificationAmount) + ".");
                    success = false;
                }
                break;
            // Transfers
            case 3:
            case 4:
            case 5:
                if (verificationAmount > selectedAccount.GetBalance())
                {
                    errorMessage.errorPopup.SetText("The balance of " +
                        selectedAccount.GetName() + " is too low to transfer " + ToMoneyFormat(verificationAmount) + ".");
                    success = false;
                }
                break;
        }

        if (!success)
        {
            if (!errorScreen.enabled)
            {
                errorScreen.enabled = true;
            }
        }

        Debug.Log("ValidateAmount() called; success = " + success);

        return success;
    }

    public void UpdateBalance()
    {
        Debug.Log("UpdateBalance() called; verificationAmount = " + verificationAmount);
        switch (navigation.functionMode)
        {
            // withdraw
            case 0:
                selectedAccount.DecreaseBalance(verificationAmount);
                break;
            // deposit
            case 1:
                selectedAccount.IncreaseBalance(verificationAmount);
                break;
            // Transfer b/w accounts
            case 3:
                selectedAccount.DecreaseBalance(verificationAmount);
                toAccount.IncreaseBalance(verificationAmount);
                break;
            // eTransfer Pay
            case 4:
                selectedAccount.DecreaseBalance(verificationAmount);
                break;
            // Pay Bills
            case 5:
                selectedAccount.DecreaseBalance(verificationAmount);
                break;
        }

        UpdateCurrentBalance(selectedAccount);
    }

    public void UpdateAccountBalance(string accountName)
    {
        selectedAccount = GetAccountFromName(accountName);

        if (selectedAccount != null)
        {
            Debug.Log("selectedAccount = " + selectedAccount.GetName());
            EnableAllButtons();
        }
        else
        {
            Debug.Log("ACCOUNT IS NULL");
            DisableAllButtons();
        }

        UpdateAccountSummary(selectedAccount);
        UpdateCurrentBalance(selectedAccount);
        UpdateButtonInteractability(selectedAccount);
    }

    private Account GetAccountFromName(string accountName)
    {
        Account account;
        switch (accountName)
        {
            case "Chequing":
                account = chequing;
                break;
            case "Savings":
                account = savings;
                break;
            case "Tax-Free Savings":
                account = taxFreeSavings;
                break;
            case "Basic Account":
                account = basicAccount;
                break;
            default:
                account = null;
                break;
        }

        return account;
    }

    public void SetRecipientFromName(string recipientName)
    {
        Recipient recipient;
        switch (recipientName)
        {
            case "Bob":
                recipient = bob;
                break;
            case "Andrea":
                recipient = andrea;
                break;
            case "Frank":
                recipient = frank;
                break;
            default:
                recipient = null;
                break;
        }

        selectedRecipient = recipient;
    }

    private void UpdateButtonInteractability(Account account)
    {
        if (account == null)
        {
            return;
        }

        if (account.GetBalance() < 100)
        {
            withdraw100.interactable = false;
        }

        if (account.GetBalance() < 80)
        {
            withdraw80.interactable = false;
        }

        if (account.GetBalance() < 60)
        {
            withdraw60.interactable = false;
        }

        if (account.GetBalance() < 40)
        {
            withdraw40.interactable = false;
        }

        if (account.GetBalance() < 20)
        {
            withdraw20.interactable = false;
        }

        if (account.GetBalance() <= 0)
        {
            withdrawCustom.interactable = false;
        }
    }

    private void EnableAllButtons()
    {
        withdraw100.interactable = true;
        withdraw80.interactable = true;
        withdraw60.interactable = true;
        withdraw40.interactable = true;
        withdraw20.interactable = true;
        withdrawCustom.interactable = true;
    }

    private void DisableAllButtons()
    {
        withdraw100.interactable = false;
        withdraw80.interactable = false;
        withdraw60.interactable = false;
        withdraw40.interactable = false;
        withdraw20.interactable = false;
        withdrawCustom.interactable = false;
    }

    private void UpdateAccountSummary(Account account)
    {
        if (account != null)
        {
            accountSummaryText.SetText(account.GetName() + " Account Summary");
        }
        else
        {
            accountSummaryText.SetText("Account Summary");
        }
    }

    private void UpdateCurrentBalance(Account account)
    {
        if (account != null)
        {
            if (navigation.functionMode == 3)
            {
                receiptInfoText.SetText("From Account: " + selectedAccount.GetName() +
                    ", Current Balance: " + ToMoneyFormat(selectedAccount.GetBalance()) +
                    "\nTo Account: " + toAccount.GetName() +
                    ", Current Balance: " + ToMoneyFormat(toAccount.GetBalance()));
            }
            else if (navigation.functionMode == 4)
            {
                receiptInfoText.SetText("From Account: " + selectedAccount.GetName() +
                    ", Current Balance: " + ToMoneyFormat(selectedAccount.GetBalance()) +
                    "\nRecipient: " + selectedRecipient.GetName());
            }
            else if (navigation.functionMode == 5)
            {
                receiptInfoText.SetText("From Account: " + selectedAccount.GetName() +
                    ", Current Balance: " + ToMoneyFormat(selectedAccount.GetBalance()) +
                    "\nPayee: " + toDropdownTransfer.options[toDropdownTransfer.value].text);
            }
            else if (navigation.functionMode == 6)
            {
                receiptInfoText.SetText("Requested " + ToMoneyFormat(verificationAmount) +
                    " from " + selectedRecipient.GetName());
            }
            else
            {
                receiptInfoText.SetText("Account: " + selectedAccount.GetName() + "\nCurrent Balance: " + ToMoneyFormat(account.GetBalance()));
            }

            amountInfoText.SetText("Current Balance: " + ToMoneyFormat(account.GetBalance()));
            checkBalanceCurrentBalanceText.SetText("Current Balance: " + ToMoneyFormat(account.GetBalance()));
            withdrawCurrentBalanceText.SetText("Current Balance: " + ToMoneyFormat(account.GetBalance()));
        }
        else
        {
            receiptInfoText.SetText("ERROR: could not find account information");
            amountInfoText.SetText("ERROR: could not find account information");
            checkBalanceCurrentBalanceText.SetText("ERROR: could not find account information");
            withdrawCurrentBalanceText.SetText("ERROR: could not find account information");
        }
    }

    private IEnumerator UpdateTransferMoneyToDropdown()
    {
        yield return new WaitForFixedUpdate();

        // TRANSFER BETWEEN ACCOUNTS
        if (navigation.functionMode == 3)
        {
            toDropdownTransfer.ClearOptions();

            // Get accounts with balances
            string account1 = chequing.GetName() + " | " + ToMoneyFormat(chequing.GetBalance());
            string account2 = savings.GetName() + " | " + ToMoneyFormat(savings.GetBalance());
            string account3 = taxFreeSavings.GetName() + " | " + ToMoneyFormat(taxFreeSavings.GetBalance());
            string account4 = basicAccount.GetName() + " | " + ToMoneyFormat(basicAccount.GetBalance());

            // Create list of accounts
            List<string> accounts = new List<string>();
            accounts.Add(account1);
            accounts.Add(account2);
            accounts.Add(account3);
            accounts.Add(account4);

            // Remove currently selected account in "from"
            accounts.Remove(fromDropdownTransfer.options[fromDropdownTransfer.value].text);

            toDropdownTransfer.AddOptions(accounts);
        }
        else if (navigation.functionMode == 4 || navigation.functionMode == 6)
        {
            toDropdownTransfer.ClearOptions();

            List<string> recipients = new List<string>();
            recipients.Add(bob.GetName());
            recipients.Add(andrea.GetName());
            recipients.Add(frank.GetName());

            toDropdownTransfer.AddOptions(recipients);
        }
        // TRANSFER - PAY BILLS
        else if (navigation.functionMode == 5)
        {
            toDropdownTransfer.ClearOptions();

            List<string> payees = new List<string>();
            payees.Add(mcMaster.GetName());
            payees.Add(electricCompany.GetName());
            payees.Add(canadianTaxRevenue.GetName());

            toDropdownTransfer.AddOptions(payees);
        }
    }

    // put float into money form (ie. 1000000.99f -> "$1,000,000.99")
    private string ToMoneyFormat(float amount)
    {
        string formatted = amount.ToString("0.00");
        int count = 0;
        for (int i = (formatted.Length - 1); i > 0; i--)
        {
            if (formatted.Length == 7)
            {
                formatted = formatted.Insert(1, ",");
                break;
            }

            if (i >= (formatted.Length - 3))
            {
                continue;
            }
            else
            {
                if (count == 0 && i > 3)
                {
                    count = 1;
                }
                else if (count >= 2)
                {
                    formatted = formatted.Insert(i, ",");
                    count = 0;
                }
                else if (count > 0)
                {
                    count++;
                }
            }
        }

        formatted = formatted.Insert(0, "$");

        return formatted;
    }
}

// Class definition for Account
class Account
{
    private string name;
    private float balance;

    public Account(string name, float balance)
    {
        this.name = name;
        this.balance = balance;
    }

    public string GetName()
    {
        return name;
    }

    public float GetBalance()
    {
        return balance;
    }

    // Increases the account balance by x amount
    public void IncreaseBalance(float amount)
    {
        balance += amount;
    }

    // Decrease the account balance by x amount, limited to minimum of $0
    public void DecreaseBalance(float amount)
    {
        balance -= amount;

        if (balance < 0)
        {
            balance = 0;
        }
    }
}

// Class definition for Recipient
class Recipient
{
    private string name;

    public Recipient(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return name;
    }
}

// Class definition for Payee
class Payee
{
    private string name;

    public Payee(string name)
    {
        this.name = name;
    }

    public string GetName()
    {
        return name;
    }
}
