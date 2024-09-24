using System;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    // 切菜声音事件
    public static event EventHandler OnAnyCut;
    new public static void ResetStaticData() => OnAnyCut = null;
    //子物体cuttingCounterVisual播放动画事件
    public event EventHandler OnCut;
    // 进度条UI进度事件
    public event EventHandler<IHasProgress.OnProdressChangedEventArges> OnProdressChanged;

    //原物品和切过的物品的对应关系以及要切的次数
    [SerializeField] private CuttingRecipe_SO[] cuttingRecipe_SOArray;
    //计数，切了几下
    private int cuttingProgress;
    // 缓存
    private CuttingRecipe_SO curCuttingRecipe_SO;

    // 主要交互
    public override void Interact(Player player)
    {
        if (myKitchenObject == null)    //自己上没有物品
        {
            if (player.HasKitchenObject()&& CanCut(player.GetKitchenObject().GetKitchenObject_SO))    //玩家身上有物品,并且这个物品是可以切的,才能放到砧板上
            {
                cuttingProgress = 0;
                OnProdressChanged?.Invoke(this, new IHasProgress.OnProdressChangedEventArges { progressNormalized = 0 });
                player.GetKitchenObject().SetPointParent(this);
            }
        }
        else    //自己上有物品
        {
            if (!player.HasKitchenObject())    //玩家身上没物品
                myKitchenObject.SetPointParent(player);
            else
            {            //玩家身上有物品并且是盘子                     
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    if (plateKitchenObject.TryAddIngredient(myKitchenObject.GetKitchenObject_SO))    //把物品加到盘子上
                        myKitchenObject.DestorySelf();
            }
        }
    }

    // 次要交互
    public override void InteractAlternate(Player player)
    {
        if (myKitchenObject != null && CanCut(myKitchenObject.GetKitchenObject_SO))    //砧板上有物品并且可以切
        {
            cuttingProgress++;
            //切菜进度
            OnProdressChanged?.Invoke(this, new IHasProgress.OnProdressChangedEventArges { progressNormalized = (float)cuttingProgress / curCuttingRecipe_SO.maxCuttingProgress });
            //播放动画
            OnCut?.Invoke(this, EventArgs.Empty);
            //切菜声音
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            if (cuttingProgress < curCuttingRecipe_SO.maxCuttingProgress)
                return;
            myKitchenObject.DestorySelf();
            //静态方法
            KitchenObject.PlaceKitchenObject(curCuttingRecipe_SO.output, this);
        }
    }

    private bool CanCut(KitchenObject_SO kitchenObject_SO)
    {
        curCuttingRecipe_SO = GetCuttingRecipe_SOByInput(kitchenObject_SO);
        return curCuttingRecipe_SO != null;
    }

    private CuttingRecipe_SO GetCuttingRecipe_SOByInput(KitchenObject_SO kitchenObject_SO)
    {
        foreach (CuttingRecipe_SO cuttingRecipe_SO in cuttingRecipe_SOArray)
            if (cuttingRecipe_SO.input == kitchenObject_SO)
                return cuttingRecipe_SO;
        return null;
    }
}
