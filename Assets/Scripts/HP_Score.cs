using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HP_Score : MonoBehaviour
{
    public bool isGameOver = false;

    // Timer Setting
    public TextMeshProUGUI timerText;
    private int totalSecs = 0;

    private void Start()
    {
        StartCoroutine(GameTimer());
    }

    private void Update()
    {
        UpdateTimerUI();
    }

    private IEnumerator GameTimer()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1f);
            totalSecs++;
        }
    }

    private void UpdateTimerUI()
    {
        int mins = totalSecs / 60;
        int secs  = totalSecs % 60;
        timerText.text = mins.ToString("00") + ":" + secs.ToString("00");
    }
}
