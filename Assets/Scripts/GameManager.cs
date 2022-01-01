using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance { get { return _instance; }}

    public GameObject carriage;
    public Camera mainCamera;
    
    public LightReserveManager lightReserveManager;
    public bool isGameOver;
    public bool isGameWin;

    [SerializeField] TextMeshProUGUI[] playerScoresUI;

    public int[] scores = new int[2];

    [SerializeField] int scoreRightKill;
    [SerializeField] int scoreWrongKill;

    public delegate void GameRestartAction();
    public static event GameRestartAction OnGameRestart;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        //DontDestroyOnLoad(gameObject);
    }


    public void GameRestart()
    {
        OnGameRestart += Restart;
        OnGameRestart();
        System.Delegate[] clientList = OnGameRestart.GetInvocationList();
        foreach (var d in clientList)
            OnGameRestart -= (d as GameRestartAction);
    }

    void Restart()
    {
        scores = new int[2];
        isGameOver = false;
        isGameWin = false;
    }

    public void Kill(int enemyId, int playerId) { 
        if (playerId == enemyId)
        {
            scores[playerId] += scoreRightKill;   
        }
        else
        {
            scores[playerId] += scoreWrongKill;
        }

        playerScoresUI[playerId].text = "Score J" + (playerId + 1).ToString()
                + ": " + scores[playerId];
    }
}
