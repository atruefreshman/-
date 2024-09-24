using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    //我是什么物体
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

    // 判断自己是不是盘子
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

     //静态方法，在生成点生成物品
    public static KitchenObject PlaceKitchenObject(KitchenObject_SO kitchenObject_SO, IKitchenObjectParent kitchenObjectParent)
    {
        KitchenObject kitchenObject = Instantiate(kitchenObject_SO.prefab).GetComponent<KitchenObject>();
        kitchenObject.SetPointParent(kitchenObjectParent);
        return kitchenObject;
    }
}