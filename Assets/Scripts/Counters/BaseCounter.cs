using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    // 放置物品声音事件
    public static event EventHandler OnAnyObjectPlaced;
    public static void ResetStaticData() => OnAnyObjectPlaced = null;
    //厨房物体放置的位置
    protected Transform kitchenObjectPoint;       
    // 自己持有的物品
    protected KitchenObject myKitchenObject;

    void Start()
    {
        kitchenObjectPoint = transform.Find("KitchenObjectPoint");
    }

    // 主要交互虚方法
    public virtual void Interact(Player player) { }
    // 次要交互虚方法
    public virtual void InteractAlternate(Player player) { }


    // 实现IKitchenObjectParent方法
    public Transform GetKitchenObjectPoint() => kitchenObjectPoint;
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        myKitchenObject = kitchenObject;
        if (kitchenObject != null)
            OnAnyObjectPlaced?.Invoke(this, EventArgs.Empty);
    }
    public KitchenObject GetKitchenObject() => myKitchenObject;
    public bool HasKitchenObject() => myKitchenObject != null;
}
