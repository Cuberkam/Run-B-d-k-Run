using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR //Burası koda dahil edilmez sadece editörde çalışacağını ifade eder.
using UnityEditor;
#endif

public class EnemyControl : MonoBehaviour
{
    public GameObject enemySpot;
    Vector3 childOnePosition;
    Vector3 childTwoPosition;
    GameObject[] goTargetPoint;
    bool distanceOnce = true;
    Vector3 mesafe;
    int mesafeSayacı = 0;
    bool ilerimi = true;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        childOnePosition = transform.GetChild(0).position;
        childTwoPosition = transform.GetChild(1).position;
        // GidilicekNoktaParent Start
        goTargetPoint = new GameObject[transform.childCount];
        for (int i = 0; i < goTargetPoint.Length; i++)// Gidilecek olan noktalarda testere ile dönmemesi için diziye kaydedip onları child dan parent a çevirdik böylelikle hem dönmüyorlar ve hâlâ kullanabiliyoruz
        {
            goTargetPoint[i] = transform.GetChild(0).gameObject; // alt nesneleri dizinin içine kaydettik. Burada getchild ın içinde 0 kullandık çünkü nesneleri üst kısma taşıyınca eksilecek ve 2.-> 1. olacak 3.->2. olacak.
            goTargetPoint[i].transform.SetParent(transform.parent); // alt nesnelerin parenti ni testerenin parentine taşıdık.
        }
        // GidilicekNoktaParent End
    }
    void Update()
    {
        Animasyon();
    }
    void Animasyon()
    {
        if (enemySpot.name == "true")
        {
            anim.SetBool("WakeUp", true);
        }
        else
        {
            anim.SetBool("WakeUp", false);
        }
        
    }
    private void FixedUpdate()
    {
        if (enemySpot.name == "true")
        {
            GoToTargetPoint();

        }
        if (enemySpot.name == "false")
        {
            if (distanceOnce)
            {
                this.mesafe = (childOnePosition - transform.position).normalized;
                distanceOnce = false;
            }
            float mesafe = Vector3.Distance(transform.position, childOnePosition);
            
            if (childOnePosition.y >= transform.position.y && childTwoPosition.y <= transform.position.y)
            {
                transform.position += this.mesafe * Time.deltaTime * 4;
                
                
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        
    }
    private void GoToTargetPoint()
    {
        if (distanceOnce)
        {
            this.mesafe = (goTargetPoint[mesafeSayacı].transform.position - transform.position).normalized;
            distanceOnce = false;
        }
        float mesafe = Vector3.Distance(transform.position, goTargetPoint[mesafeSayacı].transform.position); // Aradaki uzaklığı float cinsinden bize verir.
        
        if (ilerimi)
        {
            transform.position += this.mesafe * Time.deltaTime * 20;
        }
        else
        {
            transform.position += this.mesafe * Time.deltaTime * 4;
        }
        if (mesafe < 0.5f)
        {
            distanceOnce = true;
            if (mesafeSayacı == goTargetPoint.Length - 1)
            {
                ilerimi = false;
            }
            else if (mesafeSayacı == 0)
            {
                ilerimi = true;
            }
            if (ilerimi)
            {
                mesafeSayacı++;
            }
            else
            {
                mesafeSayacı--;
            }
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()  // Scene ekranında çizim yapmamızı sağlıyor.
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }

    }
#endif
}


