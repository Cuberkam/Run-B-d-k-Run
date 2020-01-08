using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public GameObject son;
    public GameObject end;
    int whichLevel;
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("whichLevel")) // Önceki leveli tekrar oynayıpçıkınca geçilen sonraki bölümleri silmemesi için kontrol ediyor.
        {
            PlayerPrefs.SetInt("whichLevel", SceneManager.GetActiveScene().buildIndex); // Bulunduğumuz bölümü kaydediyor.
        }
        if (PlayerPrefs.GetInt("whichLang") == 0)//Eng
        {
            son.SetActive(false);
            end.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("whichLang") == 1)//Tr
        {
            son.SetActive(true);
            end.SetActive(false);
        }
    }
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") // Player kapıya geldiğinde sıradaki levele geçiyor.
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
