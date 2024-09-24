using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    // �������巢�¼�������
    public event EventHandler OnPlayerGrabbedObject;

    //Ҫ���ɵĳ�������
    [SerializeField] protected KitchenObject_SO kitchenObject_SO;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
            return;

        // ����Ԥ���岢����
        Instantiate(kitchenObject_SO.prefab).GetComponent<KitchenObject>().SetPointParent(player);
        OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
    }
}
