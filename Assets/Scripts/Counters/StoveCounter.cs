using System;
using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgress
{
    // 控制visual和声音事件
    public event EventHandler<OnStateChangeedEventArges> OnStateChangeed;
    public class OnStateChangeedEventArges : EventArgs { public bool isFrying; }
    // 进度条UI事件
    public event EventHandler<IHasProgress.OnProdressChangedEventArges> OnProdressChanged;

    // 肉饼转换关系
    [SerializeField] private FryingRecipe_SO[] fryingRecipe_SOArray;

    private float fryingProgress;
    private FryingRecipe_SO fryingRecipe_SO;


    private void Update()
    {
        if (myKitchenObject != null)
        {
            fryingProgress += Time.deltaTime;
            if (fryingRecipe_SO != null)    //
            {
                // 进度条
                OnProdressChanged?.Invoke(this, new IHasProgress.OnProdressChangedEventArges { progressNormalized = fryingProgress / fryingRecipe_SO.maxFryingTime });
                if (fryingProgress >= fryingRecipe_SO.maxFryingTime)
                {
                    fryingProgress = 0;
                    myKitchenObject.DestorySelf();
                    KitchenObject.PlaceKitchenObject(fryingRecipe_SO.output, this);
                    // 视觉、声音
                    bool isFrying = CanBurn(fryingRecipe_SO.output);
                    OnStateChangeed?.Invoke(this, new OnStateChangeedEventArges { isFrying = isFrying });
                }
            }
        }
    }

    // 主要交互
    public override void Interact(Player player)
    {
        if (myKitchenObject == null)    //自己上没有物品
        {
            if (player.HasKitchenObject()&& CanBurn(player.GetKitchenObject().GetKitchenObject_SO))    //玩家身上有物品,并且这个物品是可以烧的,才能放到灶台上
            {
                fryingProgress = 0;
                player.GetKitchenObject().SetPointParent(this);
                OnStateChangeed?.Invoke(this, new OnStateChangeedEventArges { isFrying = true });
            }
        }
        else    //自己上有物品，玩家身上没物品
        {
            if (!player.HasKitchenObject())
            {
                myKitchenObject.SetPointParent(player);
                OnStateChangeed?.Invoke(this, new OnStateChangeedEventArges { isFrying = false });
                OnProdressChanged?.Invoke(this, new IHasProgress.OnProdressChangedEventArges { progressNormalized = 1 });
            }
            else
            {            //玩家身上有物品并且是盘子                     
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    if (plateKitchenObject.TryAddIngredient(myKitchenObject.GetKitchenObject_SO))    //把物品加到盘子上
                    {
                        myKitchenObject.DestorySelf();
                        OnStateChangeed?.Invoke(this, new OnStateChangeedEventArges { isFrying = false });
                        OnProdressChanged?.Invoke(this, new IHasProgress.OnProdressChangedEventArges { progressNormalized = 1 });
                    }    
            }
        }
    }

    private bool CanBurn(KitchenObject_SO kitchenObject_SO)
    {
        fryingRecipe_SO = GetFryingRecipe_SOByInput(kitchenObject_SO);
        return fryingRecipe_SO != null;
    }

    private FryingRecipe_SO GetFryingRecipe_SOByInput(KitchenObject_SO kitchenObject_SO)
    {
        foreach (FryingRecipe_SO frytingRecipe_SO in fryingRecipe_SOArray)
            if (frytingRecipe_SO.input == kitchenObject_SO)
                return frytingRecipe_SO;
        return null;
    }
}
