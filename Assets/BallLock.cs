using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLock : MonoBehaviour
{
    public float angle;

    // Start is called before the first frame update
    void Start()
    {
        // change position based on angle
        angle = Random.Range(0.0f, 360.0f);
        float x = 4.4f * Mathf.Sin(angle);
        float y = 4.4f * Mathf.Cos(angle);
        transform.position = new Vector3(x, y, -0.45f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // change position if successful
    public void Move()
    {
        angle = Random.Range(0.0f, 360.0f);
        float x = 4.4f * Mathf.Sin(angle);
        float y = 4.4f * Mathf.Cos(angle);
        transform.position = new Vector3(x, y, -0.45f);
    }
}
