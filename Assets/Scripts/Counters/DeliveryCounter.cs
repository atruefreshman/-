public class DeliveryCounter : BaseCounter
{
    private static DeliveryCounter instance;
    public static DeliveryCounter Instance =>instance;

    private void Awake()
    {
        instance = this;
    }

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {   
            // 玩家手上有物品并且物品是盘子
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                // 交给DeliveryManager处理订单信息
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                plateKitchenObject.DestorySelf();
            }
        }
    }
}
