using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MathControl : MonoBehaviour
{
    private System.Random random = new System.Random();

    public Boolean start_new_game = true;
    public Button[] buttons = new Button[12];
    public string operation;
    private static int MAXLIM = 200;

    public Text targetValueTxt;
    private int targetValueNum;

    private int realValue1, realValue2;
    private int clickedValue1, clickedValue2;

    private int clicksNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        operation = PlayerPrefs.GetString("operation");
        Button button;

        for (int i = 0; i < 12; i++) {
            button = GameObject.Find("Button" + (i+1).ToString()).GetComponent<Button>();
            button.onClick.AddListener(() => ComputeResult());
        }
        button = GameObject.Find("Back").GetComponent<Button>();
        button.onClick.AddListener(() => {
            SceneManager.LoadScene("Menu");
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (start_new_game) {
            targetValueNum = random.Next(10, MAXLIM);
            GetValues();
            start_new_game = false;
        }       
    }

    void GetValues()
    {
        int min_lim = (operation == "sum" || operation == "mult") ? 1 : targetValueNum;
        int max_lim = (operation == "sum" || operation == "mult") ? targetValueNum : MAXLIM;
        realValue1 = random.Next(min_lim, max_lim);

        if (operation == "sum") {
            realValue2 = targetValueNum - realValue1;
        } else if (operation == "sub") {
            realValue2 = realValue1 - targetValueNum;
        } else if (operation == "mult") {
            realValue2 = random.Next(min_lim, max_lim);
            targetValueNum = realValue1 * realValue2;
        } else {
            targetValueNum = random.Next(min_lim, max_lim);
            realValue2 = realValue1;
            realValue1 = targetValueNum * realValue2;
        }

        targetValueTxt.text = targetValueNum.ToString();
        SetButtonValues();
    }

    void SetButtonValues()
    {
        int button1 = random.Next(0, 11);
        int button2 = random.Next(0, 11);

        while(button1 == button2) {
            button1 = random.Next(0, 11);
            button2 = random.Next(0, 11);
        }

        for (int i = 0; i < 12; i++) {
            if (i == button1) {
                buttons[button1].GetComponentInChildren<Text>().text = realValue1.ToString();
            } else if (i == button2) {
                buttons[button2].GetComponentInChildren<Text>().text = realValue2.ToString();
            } else {
                int max_lim = (operation == "sum" || operation == "mult") ? targetValueNum - 1 : MAXLIM;
                buttons[i].GetComponentInChildren<Text>().text = random.Next(10, max_lim).ToString();
            }
        }        
    }

    int CalcOperation(int value1, int value2) {
        if (operation == "sum") {
            return value1 + value2;
        } else if (operation == "sub") {
            return value1 - value2;
        } else if (operation == "mult") {
            return value1 * value2;
        } else {
            return value1 / value2;
        }
    }

    void ComputeResult()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        Button button = GameObject.Find(buttonName).GetComponent<Button>();

        clicksNum++;

        if (clicksNum == 1) {
            clickedValue1 = Int32.Parse(button.GetComponentInChildren<Text>().text);
        } else if (clicksNum == 2) {
            clickedValue2 = Int32.Parse(button.GetComponentInChildren<Text>().text);
            if (CalcOperation(clickedValue1, clickedValue2) == targetValueNum) {
                targetValueTxt.text = "0";
            }
        } else {
            clicksNum = 0;
        }
    }
}
