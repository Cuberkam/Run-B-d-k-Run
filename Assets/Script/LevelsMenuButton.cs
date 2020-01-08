using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class LevelsMenuButton : MonoBehaviour
{
    public Canvas levels;
    public Text one;
    public Text two;
    public Text three;
    public Text four;
    public Text five;
    public Text back;
    private void Start()
    {
        if (PlayerPrefs.GetInt("whichLang") == 0)//Eng
        {
            one.text = "Level 1";
            two.text = "Level 2";
            three.text = "Level 3";
            four.text = "Level 4";
            five.text = "Level 5";
            back.text = "Back";
        }
        else if (PlayerPrefs.GetInt("whichLang") == 1)//Tr
        {
            one.text = "Seviye 1";
            two.text = "Seviye 2";
            three.text = "Seviye 3";
            four.text = "Seviye 4";
            five.text = "Seviye 5";
            back.text = "Geri";
        }
        //Kayıt kaçıncı bölüme kadarsa butonları görünür yapar
        for (int i = 0; i < PlayerPrefs.GetInt("whichLevel"); i++)
        {
            levels.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }//
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (EventSystem.current.currentSelectedGameObject.name == "Back" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            SceneManager.LoadScene("MainMenu");
        }
        for (int i = 1; i <= 5; i++)
        {
            if (EventSystem.current.currentSelectedGameObject.name == ("Level_" + i) && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
            {
                SceneManager.LoadScene("Level_" + i);
            }
            
        }
    }
    public void whichButton (int button)
    {
        if (button==0)
        {
            SceneManager.LoadScene("MainMenu");
        }
        for (int i = 1; i <= 5; i++)
        {
            if (button == i)
            {
                SceneManager.LoadScene("Level_" + i);
            }
        }
    }
}
