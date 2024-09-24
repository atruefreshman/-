using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerPersent();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if(!GameManager.Instance.IsGamePlaying())
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }

}
