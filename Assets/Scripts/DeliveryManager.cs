using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{

    // �ı䶩��UI�¼�
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    // �����ύ��Ч�¼�
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    private static DeliveryManager instance;
    public static DeliveryManager Instance { get => instance; }

    //���еĲ˵�
    [SerializeField] private RecipeList_SO recipeList_SO;
    //�ȴ������Ķ���
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
        if (waitingToCookRecipeList.Count < maxWaitingRecipeAmount)    //ÿ��һ��ʱ������һ�����������4��
        {
            if (spawnRecipeTimer <= 0)
            {
                spawnRecipeTimer = spawnRecipeTime;
                //  ���ѡȡһ������
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
        //��һ����ύʳ���Ƿ���϶�������ɵĶ���������һ��
        for (int i = 0; i < waitingToCookRecipeList.Count; i++)
        {
            Recipe_SO recipe_SO = waitingToCookRecipeList[i];
            //�ȶԱ��ύ��ʳ��Ͷ������ز������Ƿ����
            if (recipe_SO.kitchenObject_SOList.Count == plateKitchenObject.GetAddedKitchenObject_SOList().Count)     
            {
                bool plateContentMatchesRecipe = true;

                //����һ�Ա��������Ƿ���ͬ
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
                
                //�����ύ��ʳ����ͬ�Ķ���
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
        //�ύ�˴����ʳ��
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<Recipe_SO> GetWaitingToCookRecipeList() => waitingToCookRecipeList;
    public int GetCountOfDeliveredRecipe() => countOfDeliveredRecipe;
}
