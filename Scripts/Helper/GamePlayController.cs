using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    public Text coinText, healthText, timerText;

    private int cointScore;

    [HideInInspector]
    public bool isPlayerAlive;

    public float timerTime = 99f;


    public GameObject endPanel;
    private void Awake()
    {
        Time.timeScale = 1f;
        MakeInstance();
        coinText.text = "Coins: " + cointScore;
    }

    void Start()
    {
        endPanel.SetActive(false);
        isPlayerAlive = true;
    }

    void Update()
    {
        CountDownTimer();
    }

    public void CoinCollected()
    {
        cointScore++;
        coinText.text = "Coins: " + cointScore;
    }

    public void DisplayText(int health)
    {
        healthText.text = "Health: " + health;
    }

    void CountDownTimer()
    {
        timerTime -= Time.deltaTime;

        timerText.text = "Time: " + timerTime.ToString("F0");//норм форматнет строку
    }



    private void MakeInstance()
    {

        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        endPanel.SetActive(true);
    }
}
