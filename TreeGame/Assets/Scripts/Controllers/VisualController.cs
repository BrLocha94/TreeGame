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

    public void GameOver(int score)
    {

    }

    public void ResetUI()
    {
        fume.SetActive(true);

        countDown.text = "";
        score.text = "Score: 0";
        time.text = "Time: 00:00:00";
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

    public void UpdateTimeValue(float time)
    {

    }
}
