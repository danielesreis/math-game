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
    private static int MAXLIM = 100;

    public Text targetValueTxt;
    private int targetValueNum;

    private int realValue1, realValue2;
    private int clickedValue1, clickedValue2;

    private int clicksNum = 0;
    float timeLeft = 31.0f;
    public Text timeLeftText;
    private float pts;
    public Text ptsText;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("pts")) {
            pts = PlayerPrefs.GetFloat("pts");
            ptsText.text = pts.ToString();
        }
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
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0) {
            timeLeft = 31.0f;
            start_new_game = true;
        }
        timeLeftText.text = ((int)timeLeft).ToString();

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

        Debug.Log(clicksNum);

        if (clicksNum == 1) {
            clickedValue1 = Int32.Parse(button.GetComponentInChildren<Text>().text);
        } else if (clicksNum == 2) {
            clickedValue2 = Int32.Parse(button.GetComponentInChildren<Text>().text);
            if (CalcOperation(clickedValue1, clickedValue2) == targetValueNum) {
                pts += (int)(timeLeft);
                PlayerPrefs.SetFloat("pts", pts);

                ptsText.text = pts.ToString();
                timeLeft = 31.0f;
                start_new_game = true;
            } else {
                start_new_game = false;
            }
            clicksNum = 0;
        }
    }
}
