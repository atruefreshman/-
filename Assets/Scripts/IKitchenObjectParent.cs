using UnityEngine;


// ������Ʒ���е�Ķ�����Ҫʵ�ֵĽӿ�
public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectPoint();
    public void SetKitchenObject(KitchenObject kitchenObject);
    public KitchenObject GetKitchenObject();
    public bool HasKitchenObject();
}
