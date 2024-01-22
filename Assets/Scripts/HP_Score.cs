using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HP_Score : MonoBehaviour
{
    public bool isGameOver = false;

    // Timer Setting
    public TextMeshProUGUI timerText;
    private int totalSecs = 0;

    // HP Setting
    public GameObject HPStone;
    public int totalHP = 100;
    public int warningHP = 30;
    private int hp;

    private void Start()
    {
        hp = totalHP;
        StartCoroutine(GameTimer());
    }

    private void Update()
    {
        UpdateTimerUI();
        UpdateHPUI();
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
        if (!timerText) return;

        int mins = totalSecs / 60;
        int secs  = totalSecs % 60;
        timerText.text = mins.ToString("00") + ":" + secs.ToString("00");
    }

    private void UpdateHPUI()
    {
        if (!HPStone) return;

        if (hp >= warningHP)
        {
            float newR = 128 + (127 * (hp - warningHP) / (totalHP - warningHP));
            HPStone.GetComponent<MeshRenderer>().materials[0].color = new Color(newR, 0, 0);
        }
        else // blink
        {

        }
    }

    public void AddHP(int value) // can use negative integer value
    {
        hp += value;
        if (hp <= 0)
        {
            hp = 0;
            isGameOver = true;
        }
    }
}
