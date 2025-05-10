using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    Camera mainCam;
    private Player player;

    private TextMeshProUGUI timerUI;
    private TextMeshProUGUI playerHpUI;
    private TextMeshProUGUI resultUI;
    private GameObject resultPanel;

    private Transform[] spawnPoints;

    private float gameTimer = 0f;
    private float maxGameTimer = 20f;
    private float spawntimer = 0f;
    private float maxSpawnTimer = 1f;

    private bool isPlaying = false;
    public bool isDebug = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;

        player = FindObjectOfType<Player>();
        timerUI = GameObject.Find("TimerUI").GetComponent<TextMeshProUGUI>();

        resultPanel = GameObject.Find("ResultPanel");
        resultUI = GameObject.Find("ResultUI").GetComponent<TextMeshProUGUI>();

        playerHpUI = GameObject.Find("PlayerHpUI").GetComponent<TextMeshProUGUI>();

        spawnPoints = new Transform[transform.childCount];

        for(int i = 0; i < transform.childCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }

        resultPanel.SetActive(false);
        gameTimer = maxGameTimer;

        isPlaying = true;
    }

    void Update()
    {
        if(isPlaying && player.hp <= 0) // 패배
        {
            resultPanel.gameObject.SetActive(true);
            resultUI.text = "Game Over";
            resultUI.color = Color.red;

            isPlaying = false;
        }

        if(isPlaying && gameTimer < 0f) // 승리
        {
            resultPanel.gameObject.SetActive(true);
            resultUI.text = "Game Clear";
            resultUI.color = Color.green;

            gameTimer = 0f;
            timerUI.text = $"{gameTimer:F0}";

            isPlaying = false;
        }

        if(spawntimer < 0f)
        {
            spawntimer = maxSpawnTimer;
            SpawnProjectile();
        }

        if(!isDebug) gameTimer -= Time.deltaTime;
        if (!isDebug) spawntimer -= Time.deltaTime;

        timerUI.text = $"{gameTimer:F0}";
        playerHpUI.text = $"Hp : {player.hp}";
    }

    private void SpawnProjectile()
    {
         ProjectileType type = (ProjectileType)UnityEngine.Random.Range(0, 4);

        if(type == ProjectileType.Lerp || type == ProjectileType.Sin)
        {
            int randIndex = UnityEngine.Random.Range(0, 6);

            GameObject obj = ObjectManager.Instance.SpawnProjectile(type);
            obj.transform.position = spawnPoints[randIndex].transform.position;
        }
        else // spiral, eight
        {
            float randX = UnityEngine.Random.value;
            float randY = UnityEngine.Random.value;

            Vector3 SpawnPosition = mainCam.ViewportToWorldPoint(new Vector3(randX, randY, 0f));

            GameObject obj = ObjectManager.Instance.SpawnProjectile(type);
            obj.transform.position = new Vector3(SpawnPosition.x, SpawnPosition.y, obj.transform.position.z);
        }
    }
}