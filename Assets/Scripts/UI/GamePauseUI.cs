using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    private static GamePauseUI instance;
    public static GamePauseUI Instance => instance;

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button resumeButton;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += (object sender, System.EventArgs e) => 
        {
            Show();
        };
        GameManager.Instance.OnGameUnPaused += (object sender, System.EventArgs e) =>
        {
            gameObject.SetActive(false);
        };

        mainMenuButton.onClick.AddListener(() => 
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
            gameObject.SetActive(false);
        });
        resumeButton.onClick.AddListener(() => 
        {
            GameManager.Instance.TogglePauseGame();
        });

        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    }
}
