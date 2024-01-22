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
    public float blinkFrequencyBase = 0.1f;
    public int lossHP_whenLossOne = -10;
    private int hp;
    private bool isBlinking = false;

    private void Start()
    {
        hp = totalHP;
        StartCoroutine(GameTimer());
    }

    private void Update()
    {
        UpdateTimerUI();
        UpdateHPUI();
        CheckGameOver();
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
            Debug.Log("HP: " + hp + ", Color R: " + newR);
            HPStone.GetComponent<MeshRenderer>().materials[0].DisableKeyword("_EMISSION");
            HPStone.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", new Color(newR / 255, 0, 0));
        }
        else // blink
        {
            Debug.Log("HP: " + hp);
            if (!isBlinking) StartCoroutine(HPStoneBlink());
        }
    }

    private IEnumerator HPStoneBlink()
    {
        isBlinking = true;

        HPStone.GetComponent<MeshRenderer>().materials[0].SetColor("_Color", new Color(0.5f, 0, 0));
        HPStone.GetComponent<MeshRenderer>().materials[0].EnableKeyword("_EMISSION");
        while (isBlinking && !isGameOver)
        {
            for (float i = 0f; i < 2f; i+=0.1f)
            {
                HPStone.GetComponent<MeshRenderer>().materials[0].SetColor(
                    "_EmissionColor", new Color(0.5f, 0, 0) * i);
                yield return new WaitForSeconds(blinkFrequencyBase / ((warningHP - hp) * 5));
            }

            for (float i = 2f; i > 0f; i-=0.1f)
            {
                HPStone.GetComponent<MeshRenderer>().materials[0].SetColor(
                    "_EmissionColor", new Color(0.5f, 0, 0) * i);
                yield return new WaitForSeconds(blinkFrequencyBase / ((warningHP - hp) * 5));
            }
        }
        isBlinking = false;
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

    private void CheckGameOver()
    {
        if (isGameOver)
        {
            GetComponent<PlayerController>().canControl = false;
            GetComponent<PlanetSpawner>().canSpawn = false;
        }
    }
}
