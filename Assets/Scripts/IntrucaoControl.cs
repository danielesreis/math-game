using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class IntrucaoControl : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Aperte nos dois valores que, quando aplicada a operação matemática, resulte no valor objetivo! \n\n Você tem que escolher os dois valores em até 30 segundos! A pontuação que voce receberá é igual ao tempo restante! Se você errar 3 vezes consecutivas, sua pontuação é zerada. \n\n Seus pontos permanecem salvos após o jogo ser fechado, para que você possa comparar seus pontos com seus amigos!";
        Button button = GameObject.Find("back").GetComponent<Button>();
        button.onClick.AddListener(() => {
            SceneManager.LoadScene("Menu");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
