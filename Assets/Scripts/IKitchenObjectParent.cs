using UnityEngine;


// 具有物品持有点的东西需要实现的接口
public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectPoint();
    public void SetKitchenObject(KitchenObject kitchenObject);
    public KitchenObject GetKitchenObject();
    public bool HasKitchenObject();
}
