using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MathControl : MonoBehaviour
{
    public Boolean start_new_game = true;
    private System.Random random = new System.Random();
    public Text targetValueTxt;
    private int targetValueNum;
    private int value1, value2;
    private int clickedValue1, clickedValue2;
    private int clicksNum = 0;
    public Button[] buttons = new Button[12];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 12; i++) {
            Button button = GameObject.Find("Button" + (i+1).ToString()).GetComponent<Button>();
            button.onClick.AddListener(() => computeResult());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start_new_game) {
            targetValueNum = random.Next(10, 100);
            targetValueTxt.text = targetValueNum.ToString();
            getValues();
            start_new_game = false;
        }       
    }

    void getValues()
    {
        value1 = random.Next(targetValueNum, 200);
        value2 = value1 - targetValueNum;
        setButtonValues();
    }

    void setButtonValues()
    {
        int button1 = random.Next(0, 11);
        int button2 = random.Next(0, 11);

        while(button1 == button2) {
            button1 = random.Next(0, 11);
            button2 = random.Next(0, 11);
        }

        for (int i = 0; i < 12; i++) {
            if (i == button1) {
                buttons[button1].GetComponentInChildren<Text>().text = value1.ToString();
            } else if (i == button2) {
                buttons[button2].GetComponentInChildren<Text>().text = value2.ToString();
            } else {
                buttons[i].GetComponentInChildren<Text>().text = random.Next(10, 200).ToString();
            }
        }        
    }

    void computeResult()
    {
        String buttonName = EventSystem.current.currentSelectedGameObject.name;
        Button button = GameObject.Find(buttonName).GetComponent<Button>();

        clicksNum++;

        if (clicksNum == 1) {
            clickedValue1 = Int32.Parse(button.GetComponentInChildren<Text>().text);
            Debug.Log(clickedValue1);
        } else if (clicksNum == 2) {
            clickedValue2 = Int32.Parse(button.GetComponentInChildren<Text>().text);
            Debug.Log(clickedValue2);
            if ((clickedValue1 - clickedValue2) == targetValueNum) {
                targetValueTxt.text = "0";
            }
        } else {
            clicksNum = 0;
        }
    }
}
