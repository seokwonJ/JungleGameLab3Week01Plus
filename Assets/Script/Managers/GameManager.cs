using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance { get { return _instance; } private set { } }

    [Header("컨텐츠")]
    public bool isGameOver;
    public float playTime = 0;
    public int Level = 1;

    [Header("소환")]
    public GameObject enemySpawner;
    public GameObject obstacleSpawner;
    public GameObject cloudeSpawner;
    public GameObject boss;
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        GameStart();
    }

    void Update()
    {
        if (isGameOver) return;

        UpdateTimer();

        if (playTime > 60 && !isboss)
            BossStart();

        if (playTime > 7) // 7초 초과부터 게임 시작
        {
            GamePlaying();
        }
        else if (playTime > 180)
        {
            if (UIManager.Instance.IsReadyUI)
                UIManager.Instance.UpdateGameClearUI();
            return;
        }
    }

    public void UpdateTimer()
    {
        playTime += Time.deltaTime;
        UIManager.Instance.UpdateTimeText((int)playTime);
    }

    //public void StageLevelUp()
    //{
    //    Level += 1;
    //    UIManager.Instance.UpdateLevelText(Level);
    //    //enemySpawner.GetComponent<EnemySpawn>().spawnInterval -= 1f;
    //}

    // 게임 시작
    public void GameStart()
    {
        isboss = false;
        isGameOver = false;
        playTime = 0;
        UIManager.Instance.UpdateGameStartUI();
        if (enemySpawner != null) enemySpawner.SetActive(false);
    }

    // 게임 플레이 중 
    public void GamePlaying()
    {
        UIManager.Instance.EndGameStartUI();
        if (enemySpawner != null) enemySpawner.SetActive(true);
    }


    // 게임오버 됐을 때
    public void GameOver()
    {
        UIManager.Instance.UpdateGameOverUI();
        if (enemySpawner != null) enemySpawner.SetActive(false);
        isGameOver = true;
    }

    // 상점 씬으로 넘어감
    public void GoShop()
    {
        UIManager.Instance.UpdateGoShopUI();
        SceneManager.LoadScene(1);
    }

    // 플레이씬으로 넘어감
    public void GoGame()
    {
        GameStart();
        SceneManager.LoadScene(0);
    }

    bool isboss;
    // 보스전 시작
    public void BossStart()
    {
        if (enemySpawner != null) enemySpawner.SetActive(false);
        UIManager.Instance.UpdateGamePlayingUI();
        // 보스 스폰

        Instantiate(boss);
        isboss = true;
    }

    // 보스 클리어시
    public void BossClear()
    {
        isGameOver = true;
        // 보스 클리어 UI 활성화

    }
}