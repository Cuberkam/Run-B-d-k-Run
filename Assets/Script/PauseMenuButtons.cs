using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    public Canvas blur;
    public Canvas Menu;
    public Text exitMenu;
    public Text quit;

    private void Start()
    {
        if (PlayerPrefs.GetInt("whichLang") == 0)//Eng
        {
            exitMenu.text = "Return to Main Menu";
            quit.text = "Quit";
        }
        else if (PlayerPrefs.GetInt("whichLang") == 1)//Tr
        {
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
