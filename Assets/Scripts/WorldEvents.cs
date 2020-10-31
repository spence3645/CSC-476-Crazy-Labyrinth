using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldEvents : MonoBehaviour
{
    RotateMaze rm;
    Menu menu;

    private int stageIndex = 0;

    private int lives = 3;

    private int minutes = 0;
    private float seconds = 0f;

    [Header("Set in Inspector")]
    public GameObject ball;
    public GameObject[] stages;
    public Text t_timer;
    public Text t_stage;
    public Text t_lives;

    [Header("Set Dynamically")]
    public Vector3 ballSpawn;
    public Vector3 stagePos;

    // Start is called before the first frame update
    void Awake()
    {
        rm = this.GetComponent<RotateMaze>();
        menu = this.GetComponent<Menu>();

        ballSpawn = GameObject.Find("Ball Spawn").transform.position;
        ball.transform.position = ballSpawn;

        stagePos = GameObject.Find("Stage Spawn").transform.position;
        rm.maze = Instantiate(stages[stageIndex], stagePos, Quaternion.Euler(-1, 0, 0), null);

        t_stage.text = "Stage " + (stageIndex+1) + " of " + stages.Length;
        t_lives.text = "Lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    //Game timer to measure progress and highscore
    void Timer()
    {
        seconds = Time.timeSinceLevelLoad - (minutes * 60);

        if (seconds >= 60)
        {
            minutes += 1;
            seconds = 0;
        }

        if(seconds < 10)
        {
            t_timer.text = minutes + ":0" + Mathf.RoundToInt(seconds);
        }
        else
        {
            t_timer.text = minutes + ":" + Mathf.RoundToInt(seconds);
        }
    }

    //Load winning screen if all stages completed or load next stage and display progress through UI
    public void Win()
    {
        if (stageIndex >= stages.Length - 1)
        {
            Completed();
            return;
        }

        stageIndex++;

        t_stage.text = "Stage " + (stageIndex + 1) + " of " + stages.Length;

        Destroy(rm.maze);
        rm.maze = Instantiate(stages[stageIndex], stagePos, Quaternion.Euler(-1, 0, 0), null);

        ball.transform.position = ballSpawn;
        ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    public void UnlockHole()
    {
        rm.maze.GetComponent<MazeComponents>().renderFinish.color = Color.green;
        rm.maze.GetComponent<MazeComponents>().finishCollider.enabled = true;
    }

    //Resets ball back to start, count # of deaths
    public void Lost()
    {
        lives--;
        t_lives.text = "Lives: " + lives;

        if(lives <= 0)
        {
            Completed();
        }

        ball.transform.position = ballSpawn;
        ball.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    void Completed()
    {
        float tempSec = (minutes * 60) + seconds;
        int intSec = Mathf.RoundToInt(tempSec);

        if (PlayerPrefs.HasKey("Best Stage"))
        {
            if (PlayerPrefs.GetInt("Best Stage") < stageIndex + 1)
            {
                PlayerPrefs.SetInt("Best Stage", stageIndex);
                menu.DisplayScore(true, minutes, Mathf.RoundToInt(seconds));
                return;
            }

            else if (PlayerPrefs.GetInt("Best Stage") == stageIndex + 1)
            {
                if (PlayerPrefs.GetInt("Best Time") > intSec)
                {
                    PlayerPrefs.SetInt("Best Time", intSec);
                    menu.DisplayScore(true, minutes, Mathf.RoundToInt(seconds));
                    return;
                }
                else
                {
                    menu.DisplayScore(false, minutes, Mathf.RoundToInt(seconds));
                    return;
                }
            }

            else
            {
                menu.DisplayScore(false, minutes, Mathf.RoundToInt(seconds));
                return;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Best Time", intSec);
            PlayerPrefs.SetInt("Best Stage", stageIndex + 1);

            menu.DisplayScore(true, minutes, Mathf.RoundToInt(seconds));
        }
    }
}
