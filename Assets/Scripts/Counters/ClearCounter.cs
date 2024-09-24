
public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (myKitchenObject == null)    //�Լ���û����Ʒ�������������Ʒ
        {
            if (player.HasKitchenObject())
                player.GetKitchenObject().SetPointParent(this);
        }
        else    //�Լ�������Ʒ
        {
            if (!player.HasKitchenObject())    //�������û��Ʒ
                myKitchenObject.SetPointParent(player);
            else
            {   //�����������Ʒ����������                     
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(myKitchenObject.GetKitchenObject_SO))    //���Լ�����Ʒ�ӵ���ҵ�������
                        myKitchenObject.DestorySelf();
                }
                //�Լ���������Ʒ����������
                else if (myKitchenObject.TryGetPlate(out plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObject_SO))    //�������Ʒ�ӵ��Լ���������
                        player.GetKitchenObject().DestorySelf();
                }
            }
        }
    }
}
