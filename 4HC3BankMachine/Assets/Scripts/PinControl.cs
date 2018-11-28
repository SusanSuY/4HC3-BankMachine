using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinControl : MonoBehaviour
{

    public InputField pinInput;
    public Button one;
    public Button two;
    public Button three;
    public Button four;
    public Button five;
    public Button six;
    public Button seven;
    public Button eight;
    public Button nine;
    public Button zero;
    public Button delete;
    public int requiredDigitInputLength;
    public int maxDigitInputLength;
    public Button checkmark;
    public bool cannotEnterZero;

    private string encryptInput = "";
    public string input;
    public bool encryptPin;
    public string actualInput;

    // Use this for initialization
    void Start()
    {
        one.onClick.AddListener(taskOne);
        two.onClick.AddListener(taskTwo);
        three.onClick.AddListener(taskThree);
        four.onClick.AddListener(taskFour);
        five.onClick.AddListener(taskFive);
        six.onClick.AddListener(taskSix);
        seven.onClick.AddListener(taskSeven);
        eight.onClick.AddListener(taskEight);
        nine.onClick.AddListener(taskNine);
        zero.onClick.AddListener(taskZero);
        delete.onClick.AddListener(taskDelete);
    }

    //encrypt pin to be asterisk if encrypt is true
    void Update()
    {
        input = pinInput.text;

        if (encryptPin)
        {
            encryptInput = "";
            for (int i = 0; i < input.Length; i++)
            {
                encryptInput = encryptInput + "*";
            }
            pinInput.text = encryptInput;
        }

        UpdateButtonInteractibility();
    }

    public void ClearAllValues()
    {
        encryptInput = "";
        input = "";
        pinInput.text = "";
        actualInput = "";
    }

    // check if required # of digits are inputted
    private void UpdateButtonInteractibility()
    {
        if (requiredDigitInputLength > 0)
        {
            if (input.Length >= requiredDigitInputLength)
            {
                checkmark.interactable = true;
                DisableAllNumberButtons();
            }
            else
            {
                checkmark.interactable = false;
                EnableAllNumberButtons();
            }
        }
        else
        {
            if (input.Length <= 0)
            {
                checkmark.interactable = false;
                if (cannotEnterZero)
                {
                    zero.interactable = false;
                }
            }
            else
            {
                checkmark.interactable = true;
                if (cannotEnterZero && input.Length < maxDigitInputLength)
                {
                    zero.interactable = true;
                }
            }

            if (maxDigitInputLength > 0)
            {
                if (input.Length >= maxDigitInputLength)
                {
                    DisableAllNumberButtons();
                }
                else
                {
                    EnableAllNumberButtons();
                }
            }
        }

        if (input.Length <= 0)
        {
            delete.interactable = false;
        }
        else
        {
            delete.interactable = true;
        }
    }

    private void DisableAllNumberButtons()
    {
        zero.interactable = false;
        one.interactable = false;
        two.interactable = false;
        three.interactable = false;
        four.interactable = false;
        five.interactable = false;
        six.interactable = false;
        seven.interactable = false;
        eight.interactable = false;
        nine.interactable = false;
    }

    private void EnableAllNumberButtons()
    {
        if (!cannotEnterZero)
        {
            zero.interactable = true;
        }
        one.interactable = true;
        two.interactable = true;
        three.interactable = true;
        four.interactable = true;
        five.interactable = true;
        six.interactable = true;
        seven.interactable = true;
        eight.interactable = true;
        nine.interactable = true;
    }

    void taskOne()
    {
        pinInput.text = pinInput.text + "1";
        actualInput = actualInput + "1";
    }

    void taskTwo()
    {
        pinInput.text = pinInput.text + "2";
        actualInput = actualInput + "2";
    }

    void taskThree()
    {
        pinInput.text = pinInput.text + "3";
        actualInput = actualInput + "3";
    }

    void taskFour()
    {
        pinInput.text = pinInput.text + "4";
        actualInput = actualInput + "4";
    }

    void taskFive()
    {
        pinInput.text = pinInput.text + "5";
        actualInput = actualInput + "5";
    }

    void taskSix()
    {
        pinInput.text = pinInput.text + "6";
        actualInput = actualInput + "6";
    }

    void taskSeven()
    {
        pinInput.text = pinInput.text + "7";
        actualInput = actualInput + "7";
    }

    void taskEight()
    {
        pinInput.text = pinInput.text + "8";
        actualInput = actualInput + "8";
    }

    void taskNine()
    {
        pinInput.text = pinInput.text + "9";
        actualInput = actualInput + "9";
    }

    void taskZero()
    {
        pinInput.text = pinInput.text + "0";
        actualInput = actualInput + "0";
    }

    void taskDelete()
    {
        pinInput.text = pinInput.text.Substring(0, (pinInput.text.Length - 1));
        actualInput = actualInput.Substring(0, (actualInput.Length - 1));
    }


}
