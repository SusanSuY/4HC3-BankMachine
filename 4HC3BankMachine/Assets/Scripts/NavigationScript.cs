using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationScript : MonoBehaviour
{
    public Canvas startMenu;
    public Canvas pinScreen;
    public Canvas mainScreen;
    public Canvas depositScreen;
    public Canvas accountSelection;
    public Canvas transferScreen;
    public Canvas transferVerificationScreen;
    public Canvas receiptScreen;
    public Canvas printReceipt;
    public Canvas withdrawScreen;
    public Canvas transferMoneyScreen;
    public Canvas selectRecipentScreen;
    public Canvas checkBalanceScreen;
    public Canvas settingsScreen;
    public Canvas signoutscreen;
    public Canvas removeCardScreen;
    public Canvas selectRecipent;
    public Canvas withdrawBillScreen;
    public Canvas transferSelectScreen;
    public Canvas eTransferScreen;
    public Canvas PINSettingScreen;

    //start menu
    public Button startMenuCardInsert;
    public Button startMenuCheck;

    //pin menu
    public Button pinBack;
    public Button pinCheck;

    //main menu
    public Button deposit;
    public Button withdraw;
    public Button settings;
    public Button transfer;
    public Button checkBalance;
    public Button signout;
    // 0 = withdraw, 1 = deposit, 2 = check balance, 3 = etransfer
    public int functionMode = -1;

    //deposit screen
    public Button depositBack;
    public Button depositCash;
    public Button depositCheque;

    //account selection screen
    public Button accountChequeing;
    public Button accountSavings;
    public Button accountTaxFree;
    public Button accountBasic;
    public Button accountBack;

    //withdraw screen
    public Button withdrawBack;
    public Button withdraw20;
    public Button withdraw40;
    public Button withdraw60;
    public Button withdraw80;
    public Button withdraw100;
    public Button withdrawCustom;

    //withdraw Bills
    public Button withdrawBillConfirm;
    public Button withdrawBillBack;

    //settings screen
    public Button settingsBack;

    //transfer screen
    public Button transferBack;
    public Button transferCheck;

    //transfer verification screen
    public Button transferYes;
    public Button transferNo;

    //receipt screen
    public Button receiptPrint;
    public Button receiptEmail;
    public Button receiptMainMenu;

    //transfer select screen
    public Button transferSelectAccount;
    public Button transferSelectETransfer;
    public Button transferSelectPayBills;
    public Button transferSelectBack;
    //if true, transfer between accounts, else pay bills
    public bool accountOrBills;

    //transfer money screen;
    public Button transferMoneyBack;
    public Button transferMoneyCheck;

    // check balance screen;
    public Button checkBalanceBack;

    //TODO
    ////eTransferScreen
    //public Button eTransferSend;
    //public Button eTransferRequest;
    //public Button eTransferBack;
    ////if true, send, else request
    //public bool sendOrRequest;

    ////selectrecipient screen
    //public Button selectrecipientBack;


    //account settings

    // Use this for initialization
    void Start()
    {
        //set all to false
        startMenu.enabled = true;
        pinScreen.enabled = false;
        mainScreen.enabled = false;
        depositScreen.enabled = false;
        accountSelection.enabled = false;
        transferScreen.enabled = false;
        transferVerificationScreen.enabled = false;
        receiptScreen.enabled = false;
        printReceipt.enabled = false;
        withdrawScreen.enabled = false;
        transferMoneyScreen.enabled = false;
        selectRecipentScreen.enabled = false;
        checkBalanceScreen.enabled = false;
        settingsScreen.enabled = false;
        signoutscreen.enabled = false;
        removeCardScreen.enabled = false;
        selectRecipent.enabled = false;
        withdrawBillScreen.enabled = false;
        transferSelectScreen.enabled = false;
        eTransferScreen.enabled = false;
        PINSettingScreen.enabled = false;

        //start menu
        startMenuCardInsert.onClick.AddListener(taskInsert);
        startMenuCheck.onClick.AddListener(taskStartCheck);

        //pin menu
        pinBack.onClick.AddListener(taskPinBack);
        pinCheck.onClick.AddListener(taskPinCheck);

        //main menu
        deposit.onClick.AddListener(taskMainDeposit);
        withdraw.onClick.AddListener(taskMainWithdraw);
        settings.onClick.AddListener(taskMainSettings);
        transfer.onClick.AddListener(taskMainTransfer);
        checkBalance.onClick.AddListener(taskMainCheckBalance);
        signout.onClick.AddListener(taskMainSignout);

        //deposit menu
        depositBack.onClick.AddListener(taskDepositBack);
        depositCash.onClick.AddListener(taskDepositCash);
        depositCheque.onClick.AddListener(taskDepositCash);

        //withdraw menu
        withdrawBack.onClick.AddListener(taskWithdrawBack);
        withdraw20.onClick.AddListener(taskWithdrawBill);
        withdraw40.onClick.AddListener(taskWithdrawBill);
        withdraw60.onClick.AddListener(taskWithdrawBill);
        withdraw80.onClick.AddListener(taskWithdrawBill);
        withdraw100.onClick.AddListener(taskWithdrawBill);
        withdrawCustom.onClick.AddListener(taskWithdrawCustom);

        //withdrawBill
        withdrawBillConfirm.onClick.AddListener(taskWithdrawBillConfirm);
        withdrawBillBack.onClick.AddListener(taskWithdrawBillBack);

        //settings menu
        settingsBack.onClick.AddListener(taskSettingsBack);

        //accountSelect menu
        accountBasic.onClick.AddListener(taskAccountBasic);
        accountChequeing.onClick.AddListener(taskAccountBasic);
        accountTaxFree.onClick.AddListener(taskAccountBasic);
        accountSavings.onClick.AddListener(taskAccountBasic);
        accountBack.onClick.AddListener(taskAccountBack);

        //transfer menu
        transferBack.onClick.AddListener(taskTransferBack);
        transferCheck.onClick.AddListener(taskTransferCheck);

        //transfer verification menu
        transferYes.onClick.AddListener(taskTransferYes);
        transferNo.onClick.AddListener(taskTransferNo);

        //receipt screen
        receiptPrint.onClick.AddListener(taskReceiptPrint);
        receiptEmail.onClick.AddListener(taskReceiptPrint);
        receiptMainMenu.onClick.AddListener(taskReceiptMainMenu);

        //transfer select screen
        transferSelectAccount.onClick.AddListener(taskTransferSelectAccount);
        transferSelectETransfer.onClick.AddListener(taskTransferSelectETransfer);
        transferSelectPayBills.onClick.AddListener(taskTransferSelectPayBills);
        transferSelectBack.onClick.AddListener(taskTransferSelectBack);

        //transfer money screen
        transferMoneyBack.onClick.AddListener(taskTransferMoneyBack);
        transferMoneyCheck.onClick.AddListener(taskTransferMoneyCheck);

        // check balance screen
        checkBalanceBack.onClick.AddListener(taskCheckBalanceBack);

    }

    //start menu
    void taskInsert()
    {
        startMenu.enabled = false;
        pinScreen.enabled = true;
    }

    void taskStartCheck()
    {
        startMenu.enabled = false;
        mainScreen.enabled = true;
    }

    //pin menu
    void taskPinBack()
    {
        startMenu.enabled = true;
        pinScreen.enabled = false;
    }

    void taskPinCheck()
    {
        pinScreen.enabled = false;
        mainScreen.enabled = true;
    }

    //main menu
    void taskMainDeposit()
    {
        mainScreen.enabled = false;
        depositScreen.enabled = true;
        functionMode = 1;
    }

    void taskMainWithdraw()
    {
        mainScreen.enabled = false;
        accountSelection.enabled = true;
        functionMode = 0;
    }

    void taskMainSettings()
    {
        mainScreen.enabled = false;
        settingsScreen.enabled = true;
    }

    void taskMainTransfer()
    {
        mainScreen.enabled = false;
        transferSelectScreen.enabled = true;
        functionMode = 3;
    }

    void taskMainCheckBalance()
    {
        mainScreen.enabled = false;
        accountSelection.enabled = true;
        functionMode = 2;
    }

    void taskMainSignout()
    {
        mainScreen.enabled = false;
        signoutscreen.enabled = true;
        StartCoroutine(goBackToStart());
    }

    IEnumerator goBackToStart()
    {
        yield return new WaitForSeconds(5);
        signoutscreen.enabled = false;
        startMenu.enabled = true;
    }

    //deposit screen
    void taskDepositBack()
    {
        depositScreen.enabled = false;
        mainScreen.enabled = true;
    }

    void taskDepositCash()
    {
        depositScreen.enabled = false;
        accountSelection.enabled = true;
    }

    //withdraw screen
    void taskWithdrawBack()
    {
        withdrawScreen.enabled = false;
        accountSelection.enabled = true;
    }

    void taskWithdrawBill()
    {
        withdrawScreen.enabled = false;
        withdrawBillScreen.enabled = true;
    }

    void taskWithdrawCustom()
    {
        withdrawScreen.enabled = false;
        transferScreen.enabled = true;
    }

    //withdrawBill Screen
    void taskWithdrawBillBack()
    {
        withdrawBillScreen.enabled = false;
        withdrawScreen.enabled = true;
    }

    void taskWithdrawBillConfirm()
    {
        withdrawBillScreen.enabled = false;
        transferVerificationScreen.enabled = true;
    }

    //accountSelection
    void taskAccountBack()
    {
        accountSelection.enabled = false;
        if (functionMode == 1)
        {
            depositScreen.enabled = true;
        }
        else
        {
            mainScreen.enabled = true;
        }
    }

    void taskAccountBasic()
    {
        switch (functionMode)
        {
            //withdrawl
            case 0:
                {
                    accountSelection.enabled = false;
                    withdrawScreen.enabled = true;
                    break;
                }
            //deposit
            case 1:
                {
                    accountSelection.enabled = false;
                    transferScreen.enabled = true;
                    break;
                }
            //checkBalance
            case 2:
                {
                    accountSelection.enabled = false;
                    checkBalanceScreen.enabled = true;
                    break;
                }
        }
    }

    //transfer menu
    void taskTransferBack()
    {
        transferScreen.enabled = false;
        if (functionMode == 0)
        {
            withdrawScreen.enabled = true;
        }
        else if(functionMode == 3)
        {
            transferSelectScreen.enabled = true;
        }
        else
        {
            accountSelection.enabled = true;
        }
    }
    //if checkmark is clicked
    void taskTransferCheck()
    {

        transferVerificationScreen.enabled = true;
        transferScreen.enabled = false;
    }

    //transfer verification menu

    //if user clicks yes during verification
    void taskTransferYes()
    {
        transferVerificationScreen.enabled = false;
        receiptScreen.enabled = true;
    }

    //if user selects no during verification
    void taskTransferNo()
    {
        switch (functionMode)
        {
            //withdrawl
            case 0:
                {
                    transferVerificationScreen.enabled = false;
                    transferScreen.enabled = true;
                    break;
                }
            //deposit
            case 1:
                {
                    transferVerificationScreen.enabled = false;
                    transferScreen.enabled = true;
                    break;
                }
            //transfer
            case 3:
                {
                    transferVerificationScreen.enabled = false;
                    transferScreen.enabled = true;
                    break;
                }
        }
    }

    //receipt menu
    //print receipt
    void taskReceiptPrint()
    {
        receiptScreen.enabled = false;
        printReceipt.enabled = true;
        StartCoroutine(goBackToMenu());
    }

    //go back to main menu
    void taskReceiptMainMenu()
    {
        receiptScreen.enabled = false;
        mainScreen.enabled = true;
    }

    //go back to main menu from print
    IEnumerator goBackToMenu()
    {
        yield return new WaitForSeconds(5);
        receiptPrint.enabled = false;
        mainScreen.enabled = true;
    }

    //settings menu
    void taskSettingsBack()
    {
        settingsScreen.enabled = false;
        mainScreen.enabled = true;
    }

    //transferSelect menu
    //select between your accounts
    void taskTransferSelectAccount()
    {
        transferSelectScreen.enabled = false;
        transferMoneyScreen.enabled = true;
        accountOrBills = true;
    }

    //select eTransfer
    void taskTransferSelectETransfer()
    {
        transferSelectScreen.enabled = false;
        eTransferScreen.enabled = true;
    }

    //select pay bills
    void taskTransferSelectPayBills()
    {
        transferSelectScreen.enabled = false;
        transferMoneyScreen.enabled = true;
        accountOrBills = false;
    }

    void taskTransferSelectBack()
    {
        transferSelectScreen.enabled = false;
        mainScreen.enabled = true;
    }

    //transfer money menu

    //transfer money menu back
    void taskTransferMoneyBack()
    {
        transferMoneyScreen.enabled = false;
        transferSelectScreen.enabled = true;
    }

    //transfer money menu confirm
    void taskTransferMoneyCheck()
    {
        transferMoneyScreen.enabled = false;
        transferScreen.enabled = true;
    }

    void taskCheckBalanceBack()
    {
        checkBalanceScreen.enabled = false;
        accountSelection.enabled = true;
    }

    // ----------------------------------------------------------------------
    // Below are functions to enable/disable buttons that are only clickable if they meet a certain condition
    // ----------------------------------------------------------------------

    // Setup WithdrawBill Confirm button
    public void EnableWithdrawBillConfirm()
    {
        withdrawBillConfirm.interactable = true;
    }

    // Disable WithdrawBill Confirm button
    public void DisableWithdrawBillConfirm()
    {
        withdrawBillConfirm.interactable = false;
    }
    
}
