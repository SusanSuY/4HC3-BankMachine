using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Accounts : MonoBehaviour
{
    public TextMeshProUGUI accountSummaryText;
    public TextMeshProUGUI checkBalanceCurrentBalanceText;

    // Accounts
    private Account chequing;
    private Account savings;
    private Account taxFreeSavings;
    private Account basicAccount;

    // Use this for initialization
    void Start()
    {
        // Initialize all 4 accounts
        chequing = new Account("Chequing", 2200);
        savings = new Account("Savings", 10450);
        taxFreeSavings = new Account("Tax-Free Savings", 4300);
        basicAccount = new Account("Basic", 10);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateAccountBalance(string accountName)
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
            case "TaxFreeSavings":
                account = taxFreeSavings;
                break;
            case "BasicAccount":
                account = basicAccount;
                break;
            default:
                account = null;
                break;
        }

        UpdateAccountSummary(account);
        UpdateCurrentBalance(account);
    }

    private void UpdateAccountSummary(Account account)
    {
        accountSummaryText.SetText(account.GetName() + " Account Summary");
    }

    private void UpdateCurrentBalance(Account account)
    {
        if (account != null)
        {
            checkBalanceCurrentBalanceText.SetText("Current Balance: $" + account.GetBalance().ToString("0.00"));
        }
        else
        {
            checkBalanceCurrentBalanceText.SetText("ERROR: could not find account information");
        }
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
