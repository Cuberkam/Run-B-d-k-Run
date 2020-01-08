using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D fizik;
    Vector3 vec;
    float horizontal = 0; 
    bool jumpOnce = true;
    private Animator anim;
    public Image[] heartImage;
    int life = 3;
    private bool boolEsc = true;
    public Canvas blur;
    public Canvas Menu;
    float deadMenuTimer = 0;
    public Text goldText;
    int goldCounter;
    public int goldNumber;
    public void menuButton(int button)
    {
        if (boolEsc==false && button == 1)
        {
            Time.timeScale = 1;
            blur.enabled = false;
            Menu.enabled = false;
            boolEsc = true;
        }
        else if (boolEsc == true && button == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void Start()
    {
        goldText.GetComponent<Text>().text = goldCounter + " / " + goldNumber;
        Time.timeScale = 1; 
        anim = GetComponent<Animator>();
        fizik = GetComponent<Rigidbody2D>();
        blur.enabled = false;
        Menu.enabled = false;

    }
    private void FixedUpdate()
    {
        karakterHareket();
        // Dead Start
        if (life<=0)
        {
            Time.timeScale = 0.4f;
            deadMenuTimer += Time.deltaTime;
            if (deadMenuTimer > 0.5f)
            {
                blur.enabled = true;
                Menu.enabled = true;
                Time.timeScale = 0;
                if (PlayerPrefs.GetInt("whichLang") == 0)//Eng
                {
                    Menu.transform.GetChild(0).GetComponent<Text>().text = "Dead";
                    Menu.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Replay";
                }
                else if (PlayerPrefs.GetInt("whichLang") == 1)//Tr
                {
                    Menu.transform.GetChild(0).GetComponent<Text>().text = "ÖLDÜN";
                    Menu.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Tekrar Oyna";
                }
                Menu.transform.GetChild(1).name = "Restart";
            }
        }
        // Dead End
    }
    void Update()
    {
        Animasyon();

        //Zıplama Start
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            if (jumpOnce)
            {
                fizik.AddForce(new Vector2(0, 500));
                jumpOnce = false;
            }
        }//Zıplama End

        //PauseMenu Start
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) && boolEsc == true)
        {
            Time.timeScale = 0;
            boolEsc = false;
            blur.enabled = true;
            Menu.enabled = true;
            if (PlayerPrefs.GetInt("whichLang") == 0)//Eng
            {
                Menu.transform.GetChild(0).GetComponent<Text>().text = "Pause";
                Menu.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Continue";
            }
            else if (PlayerPrefs.GetInt("whichLang") == 1)//Tr
            {
                Menu.transform.GetChild(0).GetComponent<Text>().text = "DURAKLAT";
                Menu.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "Devam Et";
            }
            Menu.transform.GetChild(1).name = "Continue";
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7) || Input.GetKeyDown(KeyCode.JoystickButton1)) && boolEsc == false)
        {
            Time.timeScale = 1;
            boolEsc = true;
            blur.enabled = false;
            Menu.enabled = false;
        }
        //PauseMenu End

        //Pause and Dead Key Conrol Start
        if (EventSystem.current.currentSelectedGameObject.name == "Restart" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "Continue" && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            Time.timeScale = 1;
            boolEsc = true;
            blur.enabled = false;
            Menu.enabled = false;
        }
        //Pause and Dead Key Conrol End




    }
    void karakterHareket()
    {
        horizontal= Input.GetAxisRaw("Horizontal");// D'ye basınca 1 oluyor. A'ya basınca -1 oluyor.
        vec = new Vector3(horizontal * 7, fizik.velocity.y, 0);
        fizik.velocity = vec;
    }
    private void OnCollisionEnter2D(Collision2D collision)//Hazır bir metoddur. Geçirgen olmayan bir yüzeye temas edilince bu metod çalışır.
    {
        //JumpFinish Start
        horizontal = Input.GetAxisRaw("Horizontal");
        if (horizontal == 1 || horizontal == -1) // A veya D'ye basılınca Run animasyonu oynatılacak.
        {
            anim.Play("Run");
        }
        else // Hiçbir tuşa basılmadığında ise Idle animasyonu oynatılacak.
        {
            anim.Play("Idle");
        }
        jumpOnce = true;// Zıplama metodunda yaptığımız false'u burada eğer yere dokunursa tekrar zıplamasını sağladık.
        //JumpFinish End
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //LifeInc Start
        if (col.gameObject.tag == "Heart")
        {
            heartImage[life].enabled = true;
            life++;
            Destroy(col.gameObject, 0.1f);
            
        }
        //LifeInc End

        //LifeDec Start
        if (col.gameObject.tag=="Saw" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Spike") // Dokunan her düşman ve diken 1 can eksiltecek
        {
            life--;
            heartImage[life].enabled = false;
        }
        if (col.gameObject.tag=="Water") // Suya düşerse karakter doğrudan ölür.
        {
            for (int i = 0; i < life; i++)
            {
                heartImage[i].enabled = false;
            }
            life = 0;
        }
        //LifeDec End

        //Gold Start
        if (col.gameObject.tag=="Gold")
        {
            goldCounter++;
            goldText.GetComponent<Text>().text = goldCounter + " / " + goldNumber;
            Destroy(col.gameObject, 0);
        }
        //Gold End
    }
    void Animasyon() 
    {
        horizontal = Input.GetAxisRaw("Horizontal");// D(sağ)'ye basınca 1 oluyor. A(sol)'ya basınca -1 oluyor.

        if (horizontal == 0) // Karakter duruyorken
        {
            anim.SetBool("isRunning", false);
        }
        else // Karakter koşuyorsa
        {
            anim.SetBool("isRunning", true);

        }
        if(horizontal > 0) // Karakter sağa gidiyor. (D)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(horizontal < 0) // Karakter sola gidiyor. (A)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (!jumpOnce)
        {
            if (fizik.velocity.y > 0)
            {
                anim.Play("Jumping");
            }
            else if (fizik.velocity.y < 0)
            {
                anim.Play("Falling");
            }
        }
    }
    
}
