using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    // 生成的位置
    [SerializeField] private Transform container;
    // UI模板
    [SerializeField] private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeCompleted += DeliveryManager_OnRecipeCompleted;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        UpdateVisual();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())
            gameObject.SetActive(false);
        else 
            gameObject.SetActive(true);
    }

    private void DeliveryManager_OnRecipeCompleted(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == recipeTemplate)
                continue;
            Destroy(child.gameObject);
        }
        foreach (Recipe_SO recipe_SO in DeliveryManager.Instance.GetWaitingToCookRecipeList())
        {
            RecipeTemplateUI recipeTemplateUI = Instantiate(recipeTemplate, container).GetComponent<RecipeTemplateUI>();
            recipeTemplateUI.gameObject.SetActive(true);
            recipeTemplateUI.Setup(recipe_SO);
        }
    }
}
