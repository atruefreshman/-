using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    // 改变订单UI事件
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    // 两个提交音效事件
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    private static DeliveryManager instance;
    public static DeliveryManager Instance { get => instance; }

    //所有的菜单
    [SerializeField] private RecipeList_SO recipeList_SO;
    //等待制作的订单
    private List<Recipe_SO> waitingToCookRecipeList;    

    private float spawnRecipeTime = 4.5f;
    private float spawnRecipeTimer;
    private int maxWaitingRecipeAmount = 4;

    private int countOfDeliveredRecipe;

    private void Awake()
    {
        instance = this;
        waitingToCookRecipeList = new List<Recipe_SO>();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying())
            return;
        spawnRecipeTimer -= Time.deltaTime;
        if (waitingToCookRecipeList.Count < maxWaitingRecipeAmount)    //每隔一段时间生成一个订单，最多4个
        {
            if (spawnRecipeTimer <= 0)
            {
                spawnRecipeTimer = spawnRecipeTime;
                //  随机选取一个订单
                Recipe_SO recipe_SO = recipeList_SO.recipe_SOList[UnityEngine.Random.Range(0, recipeList_SO.recipe_SOList.Count)];
                waitingToCookRecipeList.Add(recipe_SO);
                
                OnRecipeSpawned?.Invoke(this,EventArgs.Empty);
            }
        }
        else
            spawnRecipeTimer = spawnRecipeTime;
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)     
    {
        //逐一检查提交食物是否符合订单待完成的订单中其中一个
        for (int i = 0; i < waitingToCookRecipeList.Count; i++)
        {
            Recipe_SO recipe_SO = waitingToCookRecipeList[i];
            //先对比提交的食物和订单中素材数量是否相等
            if (recipe_SO.kitchenObject_SOList.Count == plateKitchenObject.GetAddedKitchenObject_SOList().Count)     
            {
                bool plateContentMatchesRecipe = true;

                //再逐一对比内容物是否相同
                foreach (KitchenObject_SO recipeKitchenObject_SO in recipe_SO.kitchenObject_SOList)    
                {
                    bool ingredientFound = false;
                    foreach (KitchenObject_SO plateKitchenObject_SO in plateKitchenObject.GetAddedKitchenObject_SOList())
                        if (recipeKitchenObject_SO != plateKitchenObject_SO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    if (!ingredientFound)
                        plateContentMatchesRecipe = false;
                }
                
                //有与提交的食物相同的订单
                if (plateContentMatchesRecipe)    
                {
                    countOfDeliveredRecipe++;
                    waitingToCookRecipeList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        //提交了错误的食物
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<Recipe_SO> GetWaitingToCookRecipeList() => waitingToCookRecipeList;
    public int GetCountOfDeliveredRecipe() => countOfDeliveredRecipe;
}
