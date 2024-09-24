using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameSate
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    private static GameManager instance;
    public static GameManager Instance => instance;

    private GameSate gameState;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer = 300;
    private float maxGamePlayeringTime = 300;

    public bool gamePaused { get; private set; }

    private void Awake()
    {
        instance = this;
        gameState = GameSate.WaitingToStart;
    }

    private void Start()
    {
        // 按键绑定的事件
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        // 按E开始游戏
        if (gameState == GameSate.WaitingToStart)
        {
            gameState = GameSate.CountdownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        if (OptionsUI.Instance.gameObject.activeSelf == true)
            return;

        TogglePauseGame();
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameSate.WaitingToStart:
                // 等待按E开始游戏
                break;
            case GameSate.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0)
                {
                    gameState = GameSate.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameSate.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer <= 0)
                {
                    gameState = GameSate.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameSate.GameOver:
                break;
        }
    }

    //暂停、恢复游戏的方法
    public void TogglePauseGame()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;
            OnGameUnPaused?.Invoke(this, EventArgs.Empty);
        }
        gamePaused = !gamePaused;
    }

    public bool IsGamePlaying() => gameState == GameSate.GamePlaying;
    public bool IsCountdownToStart() => gameState == GameSate.CountdownToStart;
    public bool IsWaitingToStart() => gameState == GameSate.WaitingToStart;
    public bool IsGameOver() => gameState == GameSate.GameOver;
    public float GetCountdownToStartTimer() => Mathf.CeilToInt(countdownToStartTimer);
    public float GetGamePlayingTimerPersent() => gamePlayingTimer / maxGamePlayeringTime;


}
