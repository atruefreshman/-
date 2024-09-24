using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        playButton.Select();
    }

    private void StartGame()
    {
        Loader.Load(Loader.Scene.GameScene);
    }

    private void QuitGame() 
    {
        Application.Quit();
    }
}
