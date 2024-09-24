using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    // 给子物体发事件做动画
    public event EventHandler OnPlayerGrabbedObject;

    //要生成的厨房物体
    [SerializeField] protected KitchenObject_SO kitchenObject_SO;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
            return;

        // 生成预制体并设置
        Instantiate(kitchenObject_SO.prefab).GetComponent<KitchenObject>().SetPointParent(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
