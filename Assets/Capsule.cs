using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capsule : MonoBehaviour
{

    public float speed = 0.001f;
    public float angle = 0.0f;
    public Text ScoreText;
    public int score;
    public BallLock balllock = new BallLock();

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        angle += speed; 

        // change position based on angle
        float x = 4.4f * Mathf.Sin(angle);
        float y = 4.4f * Mathf.Cos(angle);
        //Debug.Log(angle);
        transform.position = new Vector3(x, y, -0.5f);

        // change capsule rotation based on angle
        transform.rotation = Quaternion.Euler(new Vector3(0,0,-1*180*angle/Mathf.PI));

        if (Input.GetMouseButtonDown(0)) {
            //Debug.Log("Pressed primary button.");
            speed = speed * -1;
        }
    }

    // collision with ball
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
    }

    // collision with ball
    void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log("collision");
        if (Input.GetMouseButton(0)) {
            Debug.Log("HERE");
            balllock.Move();
            score += 1;
            ScoreText.text = score.ToString();
        }
    }

}
