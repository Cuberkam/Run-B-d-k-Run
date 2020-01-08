using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuButtons : MonoBehaviour
{
    public Text play;
    public Text level;
    public Text quit;
    private void Start()
    {
        if (PlayerPrefs.GetInt("whichLang") == 0)//Eng
        {
            play.text = "Play";
            level.text = "Levels";
            quit.text = "Quit";
        }
        else if (PlayerPrefs.GetInt("whichLang") == 1)//Tr
        {
            play.text = "Oyna";
            level.text = "Bölümler";
            quit.text = "Oyundan Çık";
        }
    }
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject.name == "NewGame" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            
            if (PlayerPrefs.GetInt("whichLevel") == 0)
            {
                SceneManager.LoadScene(1);
            }
            else
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("whichLevel")); //Enson hangi bölümde kaldıysa oradan devam edecek.
            } 
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Levels" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            SceneManager.LoadScene("Levels");
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "QuitGame" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            Application.Quit();
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Settings" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            SceneManager.LoadScene("Settings");
        }
    }
    public void whichButton(int button)
    {
        switch (button)
        {
            case 1:
                {
                    if (PlayerPrefs.GetInt("whichLevel") == 0)
                    {
                        SceneManager.LoadScene(1);
                    }
                    else
                    {
                        SceneManager.LoadScene(PlayerPrefs.GetInt("whichLevel")); //Enson hangi bölümde kaldıysa oradan devam edecek.
                    }
                    break;
                }
            case 2:
                {
                    SceneManager.LoadScene("Levels");
                    break;
                }
            case 3:
                {
                    Application.Quit();
                    break;
                }
            case 4:
                {
                    SceneManager.LoadScene("Settings");
                    break;
                }
            default:
                break;
        }


    }
}
