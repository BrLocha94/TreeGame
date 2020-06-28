using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualController : MonoBehaviour
{
    [SerializeField]
    private GameObject fume;

    [Header("Ui general Text references")]
    [SerializeField]
    private Text score;
    [SerializeField]
    private Text time;
    [SerializeField]
    private Text countDown;

    public static VisualController instance;

    int currentScreen = 0;

    private void Awake()
    {
        instance = this;
    }

    public void ResetUI(int roundTime)
    {
        fume.SetActive(true);

        countDown.text = "";
        score.text = "Score: 0";
        UpdateTimeValue(roundTime);
    }

    public void StartRound()
    {
        fume.SetActive(false);
    }

    public void SetCountDownTimer(int score)
    {
        countDown.text = score.ToString();
    }

    public void UpdateScoreValue(int value)
    {
        score.text = "Score: " + value.ToString();
    }

    public void UpdateTimeValue(float value)
    {
        int minutes = Mathf.FloorToInt(value / 60f);
        int seconds = Mathf.FloorToInt(value - minutes * 60);
        int hundredth = Mathf.FloorToInt((value * 100) % 100);

        string curTime = string.Format("Time: " + "{0:0}:{1:00}:{2:00}", minutes, seconds, hundredth);

        time.text = curTime;
    }
}
