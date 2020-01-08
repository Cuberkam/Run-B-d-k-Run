using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScene : MonoBehaviour
{
    public Text thanks;
    public Text created;
    public Text exitMenu;
    public Text quit;

    private void Start()
    {
        if (PlayerPrefs.GetInt("whichLang") == 0)//Eng
        {
            thanks.text = "Thanks for Playing";
            created.text = "Created by Erkam Kumru";
            exitMenu.text = "Return to Main Menu";
            quit.text = "Quit";
        }
        else if (PlayerPrefs.GetInt("whichLang") == 1)//Tr
        {
            thanks.text = "Oynadığınız için Teşekkürler";
            created.text = "Yapan : Erkam Kumru";
            exitMenu.text = "Ana Menüye Dön";
            quit.text = "Oyundan Çık";
        }
    }
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "ExitToMainMenu" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "QuitGame" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            Application.Quit();
        }
    }
    public void whichButton(int button)
    {
        if (button == 1)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else if (button == 2)
        {
            Application.Quit();
        }
    }
}
