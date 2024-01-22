using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HP_Score : MonoBehaviour
{
    public bool isGameOver = false;

    // Timer Setting
    public TextMeshProUGUI timerText;
    private int secs = 0;

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
            secs++;
        }
    }

    private void UpdateTimerUI()
    {
        int mins = secs / 60;
        secs %= 60;
        timerText.text = mins.ToString("00") + ":" + secs.ToString("00");
    }
}
