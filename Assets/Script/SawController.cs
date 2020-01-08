 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR //Burası koda dahil edilmez sadece editörde çalışacağını ifade eder.
using UnityEditor;
#endif

public class SawController : MonoBehaviour
{
    GameObject[] goTargetPoint;
    bool distanceOnce = true;
    Vector3 mesafe;
    int mesafeSayacı = 0;
    bool ilerimi = true;

    void Start()
    {
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
        
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, 0, 5); // Saw'un sürekli dönmesini sağlıyor. Tabi FixedUpdate'in içinde yazıldığı zaman.
        GoToTargetPoint();
    }
    void GoToTargetPoint()
    {
        if (distanceOnce)
        {
            this.mesafe = (goTargetPoint[mesafeSayacı].transform.position - transform.position).normalized;
            distanceOnce = false;
        }
        float mesafe = Vector3.Distance(transform.position, goTargetPoint[mesafeSayacı].transform.position); // Aradaki uzaklığı float cinsinden bizerir.
        transform.position += this.mesafe * Time.deltaTime * 5;
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



#if UNITY_EDITOR
[CustomEditor(typeof(SawController))]
[System.Serializable]
class SawEditor : Editor
{
    public override void OnInspectorGUI() // Bunun içine yazılan kodlar Inspector panelinin içinde çalışacak.
    {
        SawController script = (SawController)target; // Testere class ının içindeki public değişkenlere ulaşmamızı sağlıyor.
        if (GUILayout.Button("ADD", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject newGameObject = new GameObject();
            newGameObject.transform.parent = script.transform;
            newGameObject.transform.position = script.transform.position;
            newGameObject.transform.name = "TargetPoint_" + script.transform.childCount.ToString();
        }
    }
}
#endif