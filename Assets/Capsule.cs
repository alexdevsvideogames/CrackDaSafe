using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Capsule : MonoBehaviour
{
    public Text ScoreText;
    public Text HiScoreText;
    public Text GameOverText;

    public BallLock balllock = new BallLock();
    public BonusLock bonuslock = new BonusLock();

    public float speed = 0.001f;
    public int dir = 1;
    public float angle = 0.0f;
    public int score = 0;
    public int Lives = 3;

    public Image Life1;
    public Image Life2;
    public Image Life3;

    bool GameOverFlag;
    bool overlap = false;
    string CollidingWith;

    public AudioSource miss;
    public AudioSource hit;
    public AudioSource bonus;
    private AudioSource source;
    private float hitVol = .2F;
    public AudioSource gameaudio;

    void Awake ()
    {
        HiScoreText.text = PlayerPrefs.GetInt("HiScore", 0).ToString();
        source = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        dir = 1;
        Lives = 3;
        speed = 0.02f;
        angle = 0.0f;
        ScoreText.text = score.ToString();

        Life1.enabled = true;
        Life2.enabled = true;
        Life3.enabled = true;
        GameOverText.enabled = false;
        GameOverFlag = false;
        CollidingWith = "nothing";

        gameaudio = GetComponent<AudioSource>();
        gameaudio.Play();

        Screen.fullScreen = false;
        Screen.SetResolution(1200, 600, Screen.fullScreen);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        // restart from gameover
        if (GameOverFlag)
        {
            if (Input.GetMouseButtonDown(0)) {
                Start();
                GameOverFlag = false;
            }
        }
        else
        {
            angle += (dir * speed); 

            // change position based on angle
            float x = 4.4f * Mathf.Sin(angle);
            float y = 4.4f * Mathf.Cos(angle);
            //Debug.Log(angle);
            transform.position = new Vector3(x, y, -0.5f);

            // change capsule rotation based on angle
            transform.rotation = Quaternion.Euler(new Vector3(0,0,-1*180*angle/Mathf.PI));

            if (Input.GetMouseButtonDown(0)) {
                dir = dir * -1;
                Debug.Log(overlap);
                if (overlap) {
                    Debug.Log(CollidingWith);
                    if (CollidingWith == "BallLock")
                    {
                        balllock.Move();
                        bonuslock.spawn_bonus();
                        score += 1;
                        ScoreText.text = score.ToString();
                        hit.Play();
                        speed += 0.001f;
                    } else if (CollidingWith == "BonusLock")
                    {
                        bonuslock.Reset();
                        score += 5;
                        ScoreText.text = score.ToString();
                        bonus.Play();
                        speed += 0.001f;
                    }
                } else {
                    Lives -=1 ;
                    miss.Play();
                }
            }

            if(score>PlayerPrefs.GetInt("HiScore", 0))
            {
                PlayerPrefs.SetInt("HiScore", score);
                HiScoreText.text = score.ToString();
            }

            // update life tally
            update_life_tally(Lives);
        }
    }

    private void OnCollisionStay2D (Collision2D coll)
    {
        overlap = true;
        CollidingWith = coll.gameObject.name;
    }

    private void OnCollisionExit2D (Collision2D coll)
    {
        overlap = false;
        CollidingWith = "nothing";
    }

    // collision with ball
    /*void OnCollisionStay2D(Collision2D coll)
    {
        //Debug.Log("collision");
        if (Input.GetMouseButton(0)) {
            Debug.Log("HERE");
            balllock.Move();
            score += 1;
            ScoreText.text = score.ToString();
        }
    }*/

    void update_life_tally(int lives) 
    {
        if (lives == 3) {
            Life1.enabled = true;
            Life2.enabled = true;
            Life3.enabled = true;
        } else if (lives == 2) {
            Life1.enabled = true;
            Life2.enabled = true;
            Life3.enabled = false;
        } else if (lives == 1) {
            Life1.enabled = true;
            Life2.enabled = false;
            Life3.enabled = false;
        }  else if (lives == 0) {
            Life1.enabled = false;
            Life2.enabled = false;
            Life3.enabled = false;
        } else {
            GameOverText.enabled = true;
            game_over();
        }
    }

    void game_over()
    {
        angle = 0.0f;
        speed = 0.0f;
        GameOverText.enabled = true;
        GameOverFlag = true;
    }

}
