using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    // ������Ʒ�����¼�
    public static event EventHandler OnAnyObjectPlaced;
    public static void ResetStaticData() => OnAnyObjectPlaced = null;
    //����������õ�λ��
    protected Transform kitchenObjectPoint;       
    // �Լ����е���Ʒ
    protected KitchenObject myKitchenObject;

    void Start()
    {
        kitchenObjectPoint = transform.Find("KitchenObjectPoint");
    }

    // ��Ҫ�����鷽��
    public virtual void Interact(Player player) { }
    // ��Ҫ�����鷽��
    public virtual void InteractAlternate(Player player) { }


    // ʵ��IKitchenObjectParent����
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
