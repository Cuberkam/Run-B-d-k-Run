using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public GameObject keyboard;
    public GameObject controller;
    public GameObject save;
    public GameObject lang;
    private int whichLang;
    public Text back;
    public Text keyboardButton;
    public Text keyboardTitle;
    public Text keyboardLeft;
    public Text keyboardJump;
    public Text keyboardRight;
    public Text keyboardEsc;
    public Text gamepadButton;
    public Text gamepadTitle;
    public Text gamepadLeft;
    public Text gamepadJump;
    public Text gamepadRight;
    public Text gamepadEsc;
    public Text saveButton;
    public Text saveTitle;
    public Text saveDelButton;
    public Text langButton;
    public Text langTitle;

    private void Start()
    {
        controller.SetActive(false);
        save.SetActive(false);
        lang.SetActive(false);
        if (PlayerPrefs.GetInt("whichLang") == 0)//Eng
        {
            LangEng();
        }
        else if (PlayerPrefs.GetInt("whichLang") == 1)//Tr
        {
            LangTr();
        }
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
        else if (EventSystem.current.currentSelectedGameObject.name == "KeyboardButton" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            keyboard.SetActive(true);
            controller.SetActive(false);
            save.SetActive(false);
            lang.SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "ControllerButton" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            keyboard.SetActive(false);
            controller.SetActive(true);
            save.SetActive(false);
            lang.SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "SaveButton" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            keyboard.SetActive(false);
            controller.SetActive(false);
            save.SetActive(true);
            lang.SetActive(false);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "SaveButtonDel" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            PlayerPrefs.DeleteKey("whichLevel");
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "LangButton" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            keyboard.SetActive(false);
            controller.SetActive(false);
            save.SetActive(false);
            lang.SetActive(true);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Eng" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {//Eng Lang
            PlayerPrefs.SetInt("whichLang", 0);
            LangEng();
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Tr" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {//Tr Lang
            PlayerPrefs.SetInt("whichLang", 1);
            LangTr();
        }

    }
    public void whichButton(int button)
    {
        switch (button)
        {
            case 0:
                {
                    SceneManager.LoadScene("MainMenu");
                    break;
                }
            case 1://Keyboard Controls Buttın
                {
                    keyboard.SetActive(true);
                    controller.SetActive(false);
                    save.SetActive(false);
                    lang.SetActive(false);
                    break;
                }
            case 2://Gamepad Controls Button
                {
                    keyboard.SetActive(false);
                    controller.SetActive(true);
                    save.SetActive(false);
                    lang.SetActive(false);
                    break;
                }
            case 3://Save Button
                {
                    keyboard.SetActive(false);
                    controller.SetActive(false);
                    save.SetActive(true);
                    lang.SetActive(false);
                    break;
                }
            case 4://Lang Button
                {
                    keyboard.SetActive(false);
                    controller.SetActive(false);
                    save.SetActive(false);
                    lang.SetActive(true);
                    break;
                }
            case 5://Bütün kayıtları siler.
                {
                    PlayerPrefs.DeleteKey("whichLevel");
                    break;
                }
            case 6://Eng Lang
                {
                    PlayerPrefs.SetInt("whichLang", 0);
                    LangEng();
                    break;
                }
            case 7://Tr Lang
                {
                    PlayerPrefs.SetInt("whichLang", 1);
                    LangTr();
                    break;
                }
            default:
                break;
        }
        
    }
    private void LangEng()
    {
        back.text = "Back";
        keyboardButton.text = "Keyboard Keys";
        keyboardTitle.text = "Keyboard Keys";
        keyboardLeft.text = "Left";
        keyboardJump.text = "Jump";
        keyboardRight.text = "Right";
        keyboardEsc.text = "Pause";
        gamepadButton.text = "Controller Keys";
        gamepadTitle.text = "Controller Keys";
        gamepadLeft.text = "Left";
        gamepadJump.text = "Jump";
        gamepadRight.text = "Right";
        gamepadEsc.text = "Pause";
        saveButton.text = "Save";
        saveTitle.text = "Save";
        saveDelButton.text = "Save Delete";
        langButton.text = "Language";
        langTitle.text = "Language";
    }
    private void LangTr()
    {
        back.text = "Geri";
        keyboardButton.text = "Klavye Kontrolleri";
        keyboardTitle.text = "Klavye Kontrolleri";
        keyboardLeft.text = "Sola Git";
        keyboardJump.text = "Zıpla";
        keyboardRight.text = "Sağa Git";
        keyboardEsc.text = "Duraklat";
        gamepadButton.text = "Gamepad Kontrolleri";
        gamepadTitle.text = "Gamepad Kontrolleri";
        gamepadLeft.text = "Sola Git";
        gamepadJump.text = "Zıpla";
        gamepadRight.text = "Sağa Git";
        gamepadEsc.text = "Duraklat";
        saveButton.text = "Kayıt";
        saveTitle.text = "Kayıt";
        saveDelButton.text = "Kayıtları Sil";
        langButton.text = "Dil";
        langTitle.text = "Dil";
    }
}
