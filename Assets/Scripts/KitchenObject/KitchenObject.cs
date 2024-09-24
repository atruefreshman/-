using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    //����ʲô����
    [SerializeField] private KitchenObject_SO kitchenObject_SO;

    public KitchenObject_SO GetKitchenObject_SO => kitchenObject_SO;

    private IKitchenObjectParent holder;
    public IKitchenObjectParent GetHolder() => holder;

    public void SetPointParent(IKitchenObjectParent holder)
    {
        if (this.holder != null)
            this.holder.SetKitchenObject(null);

        this.holder = holder;
        this.holder.SetKitchenObject(this);
        transform.parent = this.holder.GetKitchenObjectPoint();
        transform.localPosition = Vector3.zero;
    }

    public void DestorySelf()
    {
        holder.SetKitchenObject(null);
        Destroy(gameObject);
    }

    // �ж��Լ��ǲ�������
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) 
    {
        if (this is PlateKitchenObject) 
        {
            plateKitchenObject=this as PlateKitchenObject;
            return true;
        }
        plateKitchenObject = null;
        return false;
    }

     //��̬�����������ɵ�������Ʒ
    public static KitchenObject PlaceKitchenObject(KitchenObject_SO kitchenObject_SO, IKitchenObjectParent kitchenObjectParent)
    {
        KitchenObject kitchenObject = Instantiate(kitchenObject_SO.prefab).GetComponent<KitchenObject>();
        kitchenObject.SetPointParent(kitchenObjectParent);
        return kitchenObject;
    }
}