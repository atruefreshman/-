
public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (myKitchenObject == null)    //自己上没有物品，玩家身上有物品
        {
            if (player.HasKitchenObject())
                player.GetKitchenObject().SetPointParent(this);
        }
        else    //自己上有物品
        {
            if (!player.HasKitchenObject())    //玩家身上没物品
                myKitchenObject.SetPointParent(player);
            else
            {   //玩家身上有物品并且是盘子                     
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(myKitchenObject.GetKitchenObject_SO))    //把自己的物品加到玩家的盘子上
                        myKitchenObject.DestorySelf();
                }
                //自己身上有物品并且是盘子
                else if (myKitchenObject.TryGetPlate(out plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObject_SO))    //把玩家物品加到自己的盘子上
                        player.GetKitchenObject().DestorySelf();
                }
            }
        }
    }
}
