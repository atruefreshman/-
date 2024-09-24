using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    private TextMeshProUGUI startCountdown;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        startCountdown = GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        startCountdown.text = GameManager.Instance.GetCountdownToStartTimer().ToString();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStart())
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }
}
