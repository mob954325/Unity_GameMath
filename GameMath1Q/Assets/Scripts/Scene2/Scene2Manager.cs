using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scene2Manager : MonoBehaviour
{
    private Player2 player;

    private TextMeshProUGUI timerUI;
    private TextMeshProUGUI playerHpUI;
    private TextMeshProUGUI resultUI;
    private TextMeshProUGUI resultTimerUI;
    private GameObject resultPanel;

    private float gameTimer = 0f;
    private bool isPlaying = false;

    void Start()
    {
        player = FindObjectOfType<Player2>();
        timerUI = GameObject.Find("TimerUI").GetComponent<TextMeshProUGUI>();

        resultPanel = GameObject.Find("ResultPanel");
        resultUI = GameObject.Find("ResultUI").GetComponent<TextMeshProUGUI>();

        playerHpUI = GameObject.Find("PlayerHpUI").GetComponent<TextMeshProUGUI>();
        resultTimerUI = GameObject.Find("ResultTimerUI").GetComponent<TextMeshProUGUI>();

        resultPanel.SetActive(false);
        isPlaying = true;
    }

    void Update()
    {
        if (isPlaying && player.hp <= 0) // ÆÐ¹è
        {
            resultPanel.gameObject.SetActive(true);
            resultUI.text = "Game Over";
            resultUI.color = Color.red;

            resultTimerUI.text = $"Survive Time : {gameTimer:F0}";
            isPlaying = false;
        }

        if(isPlaying) gameTimer += Time.deltaTime;

        timerUI.text = $"{gameTimer:F0}";
        playerHpUI.text = $"Hp : {player.hp}";
    }
}
