using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeCountText;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            recipeCountText.text = DeliveryManager.Instance.GetCountOfDeliveredRecipe().ToString();
            gameObject.SetActive(true);
        }
        else
            gameObject.SetActive(false);
    }

}
