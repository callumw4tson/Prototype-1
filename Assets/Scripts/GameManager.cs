using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject[] oldObject;
    public GameObject[] warObjects;
    public GameObject[] modernObjects;
    public bool oldTime = false;
    public bool warTime = false;
    public bool modernTime = true;
    private float objectSpawnDelay = 3f;
    public Slider timeIndicator;
    public GameObject[] hotbar = new GameObject[4];



    float m_LastPressTime;
    float m_PressDelay = 2.0f;

    private void Awake()
    {
        oldObject = GameObject.FindGameObjectsWithTag("FirstEraObjects");
        warObjects = GameObject.FindGameObjectsWithTag("SecondEraObjects");
        modernObjects = GameObject.FindGameObjectsWithTag("ThirdEraObjects");
       
    }


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < oldObject.Length; i++)
        {
            oldObject[i].SetActive(false);
        }
        for (int i = 0; i < warObjects.Length; i++)
        {
            warObjects[i].SetActive(false);
        }
        timeIndicator.value = 2;



        

    }
    
    // Update is called once per frame
    void Update()
    {
        
        //temp input for time travel
        if (Input.GetKeyDown(KeyCode.R) && (m_LastPressTime + m_PressDelay > Time.unscaledTime))
        {
            timeTravelForward();            
        }
        if (Input.GetKeyDown(KeyCode.E) && (m_LastPressTime + m_PressDelay > Time.unscaledTime))
        {
            timeTravelBack();
        }
        m_LastPressTime = Time.unscaledTime;
    }
    //determines the time period and disables room objects depending on the time period
    void timeTravelForward()
    {
        if (oldTime)
        {
            StartCoroutine(warEra());
            timeIndicator.value = 1;
        }
        if (warTime)
        {
            StartCoroutine(modernEra());
            timeIndicator.value = 2;
        }
        if (modernTime)
        {            
            StartCoroutine(oldEra());
            timeIndicator.value = 0;
        }
        
    }
    void timeTravelBack()
    {
        if (modernTime)
        {
            StartCoroutine(warEra());
            timeIndicator.value = 1;
        }
        if (oldTime)
        {
            StartCoroutine(modernEra());
            timeIndicator.value = 2;
        }
        if (warTime)
        {
            StartCoroutine(oldEra());
            timeIndicator.value = 0;
        }

    }
    //despawns modern items and enables old items
    IEnumerator oldEra()
    {
        modernTime = false;
        warTime = false;
        //disable
        for (int i = 0; i < modernObjects.Length; i++)
        {
            modernObjects[i].SetActive(false);

            yield return new WaitForSeconds(objectSpawnDelay / modernObjects.Length);
        }

        for (int i = 0; i < warObjects.Length; i++)
        {
            warObjects[i].SetActive(false);

            yield return new WaitForSeconds(objectSpawnDelay / warObjects.Length);
        }
        //enable
        for (int i = 0; i < oldObject.Length; i++)
        {
            oldObject[i].SetActive(true);

            yield return new WaitForSeconds(objectSpawnDelay / oldObject.Length);
        }
        oldTime = true;
    }
    //despawns old items and enables war items
    IEnumerator warEra()
    {
        oldTime = false;
        modernTime = false;
        //disable
        for (int i = 0; i < oldObject.Length; i++)
        {
            oldObject[i].SetActive(false);

            yield return new WaitForSeconds(objectSpawnDelay / oldObject.Length);
        }
        for (int i = 0; i < modernObjects.Length; i++)
        {
            modernObjects[i].SetActive(false);

            yield return new WaitForSeconds(objectSpawnDelay / modernObjects.Length);
        }
        //enable
        for (int i = 0; i < warObjects.Length; i++)
        {
            warObjects[i].SetActive(true);

            yield return new WaitForSeconds(objectSpawnDelay / warObjects.Length);
        }
        warTime = true;
    }
    //despawns war items and enables modern items
    IEnumerator modernEra()
    {
        warTime = false;
        oldTime = false;
        //disable
        for (int i = 0; i < warObjects.Length; i++)
        {
            warObjects[i].SetActive(false);

            yield return new WaitForSeconds(objectSpawnDelay / warObjects.Length);
        }

        for (int i = 0; i < oldObject.Length; i++)
        {
            oldObject[i].SetActive(false);

            yield return new WaitForSeconds(objectSpawnDelay / oldObject.Length);
        }

        //enable
        for (int i = 0; i < modernObjects.Length; i++)
        {
            modernObjects[i].SetActive(true);

            yield return new WaitForSeconds(objectSpawnDelay / modernObjects.Length);
        }
        modernTime = true;
    }
}
