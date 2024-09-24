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
            // �����������Ʒ������Ʒ������
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                // ����DeliveryManager��������Ϣ
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                plateKitchenObject.DestorySelf();
            }
        }
    }
}
