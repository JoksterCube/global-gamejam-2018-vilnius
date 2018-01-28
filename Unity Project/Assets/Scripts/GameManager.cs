using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeTillFirstSpawn;
    public int requiredProtectees;

    public Player player;

    int finished = 0;

    ProtecteeManager protecteeManager;
    WaveManager waveManager;
    EnemyManager enemyManager;
    UIManager uiManager;

    private void Awake()
    {
        protecteeManager = GetComponentInChildren<ProtecteeManager>();
        waveManager = GetComponentInChildren<WaveManager>();
        enemyManager = GetComponentInChildren<EnemyManager>();
        uiManager = GetComponentInChildren<UIManager>();
    }

    public int TotalProtectees
    {
        get
        {
            return waveManager.ProtecteesLeftFromWave(0);
        }
    }

    public void Start()
    {
        player.OnDeath -= GameOver;
        player.OnDeath += GameOver;
        UpdateProtectee();
        StartCoroutine(CountDown());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator CountDown()
    {
        int lastNumber = 0;
        while (timeTillFirstSpawn > 0)
        {
            timeTillFirstSpawn -= Time.deltaTime;
            int displayNumber = (int)timeTillFirstSpawn;
            if (displayNumber != lastNumber)
            {
                if (displayNumber == 0)
                {
                    uiManager.ShowMessage(displayNumber.ToString(), true, .5f);
                }
                else
                {
                    uiManager.ShowMessage(displayNumber.ToString());
                }
                lastNumber = displayNumber;
            }
            yield return null;
        }
        waveManager.StartGame();
    }

    public void OneFinished()
    {
        finished++;
        UpdateProtectee();
    }

    void UpdateProtectee()
    {
        string message = finished + "/" + requiredProtectees;
        uiManager.ProtecteeTextUpdate(message);
        uiManager.ProtecteeCarUpdate(finished / requiredProtectees);
        if (finished >= requiredProtectees)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene(0);
    }
}
