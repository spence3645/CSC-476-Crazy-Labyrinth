using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Text newTime;
    public Text time;
    public GameObject bestScorePanel;
    public GameObject resultPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Retry();
        }
    }

    public void DisplayScore(bool newRecord, int currentMin, int currentSec)
    {
        if (newRecord)
        {
            Time.timeScale = 0;
            bestScorePanel.SetActive(true);

            if (currentSec < 10)
            {
                newTime.text = "0" + currentMin + ":0" + currentSec;
            }
            else
            {
                newTime.text = "0" + currentMin + ":" + currentSec;
            }
        }
        else
        {
            Time.timeScale = 0;
            resultPanel.SetActive(true);

            if(currentSec < 10)
            {
                time.text = "0" + currentMin + ":0" + currentSec;
            }
            else
            {
                time.text = "0" + currentMin + ":" + currentSec;
            }
        }
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
