using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public Text title;
    private string[] operations = new string[4] {"sum", "sub", "mult", "div"};
    // Start is called before the first frame update
    void Start()
    {
        title.text = "OPERAÇÃO: MATEMÁTICA";
        Button button;
        for (int i = 0; i < 4; i++) {
            button = GameObject.Find(operations[i]).GetComponent<Button>();
            button.onClick.AddListener(() => SetOperationType());
        }
        button = GameObject.Find("inst").GetComponent<Button>();
        button.onClick.AddListener(() => {
            SceneManager.LoadScene("Instrucao");
        });
    }

    void SetOperationType()
    {
        string buttonName = EventSystem.current.currentSelectedGameObject.name;
        PlayerPrefs.SetString("operation", buttonName);
        SceneManager.LoadScene("Game");
    }

    // Update is called once per frame
    void Update()
    {
           
    }
}
