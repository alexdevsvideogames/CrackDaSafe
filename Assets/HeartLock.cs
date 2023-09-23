using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartLock : MonoBehaviour
{
    float angle;
    public GameObject capsule;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(-18.0f,3.0f,-0.45f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn bonus at flat rate
    public void spawn_bonus()
    {
        Debug.Log(capsule.GetComponent<Capsule>().Lives);
        if (capsule.GetComponent<Capsule>().Lives < 3) {
            float chance = Random.Range(0.0f,100.0f);
            //Debug.Log(chance);
            if (chance < 50.0f) 
            {
                BonusMove();
            } else {
                Start();
            }
        }
    }

    public void BonusMove()
    {
        angle = Random.Range(0.0f, 360.0f);
        float x = 4.4f * Mathf.Sin(angle);
        float y = 4.4f * Mathf.Cos(angle);
        transform.position = new Vector3(x, y, -0.55f);
    }

    public void Reset()
    {
        transform.position = new Vector3(-18.0f,3.0f,-0.45f);
    }
}
