using System;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    // �в������¼�
    public static event EventHandler OnAnyCut;
    new public static void ResetStaticData() => OnAnyCut = null;
    //������cuttingCounterVisual���Ŷ����¼�
    public event EventHandler OnCut;
    // ������UI�����¼�
    public event EventHandler<IHasProgress.OnProdressChangedEventArges> OnProdressChanged;

    //ԭ��Ʒ���й�����Ʒ�Ķ�Ӧ��ϵ�Լ�Ҫ�еĴ���
    [SerializeField] private CuttingRecipe_SO[] cuttingRecipe_SOArray;
    //���������˼���
    private int cuttingProgress;
    // ����
    private CuttingRecipe_SO curCuttingRecipe_SO;

    // ��Ҫ����
    public override void Interact(Player player)
    {
        if (myKitchenObject == null)    //�Լ���û����Ʒ
        {
            if (player.HasKitchenObject()&& CanCut(player.GetKitchenObject().GetKitchenObject_SO))    //�����������Ʒ,���������Ʒ�ǿ����е�,���ܷŵ������
            {
                cuttingProgress = 0;
                OnProdressChanged?.Invoke(this, new IHasProgress.OnProdressChangedEventArges { progressNormalized = 0 });
                player.GetKitchenObject().SetPointParent(this);
            }
        }
        else    //�Լ�������Ʒ
        {
            if (!player.HasKitchenObject())    //�������û��Ʒ
                myKitchenObject.SetPointParent(player);
            else
            {            //�����������Ʒ����������                     
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    if (plateKitchenObject.TryAddIngredient(myKitchenObject.GetKitchenObject_SO))    //����Ʒ�ӵ�������
                        myKitchenObject.DestorySelf();
            }
        }
    }

    // ��Ҫ����
    public override void InteractAlternate(Player player)
    {
        if (myKitchenObject != null && CanCut(myKitchenObject.GetKitchenObject_SO))    //���������Ʒ���ҿ�����
        {
            cuttingProgress++;
            //�в˽���
            OnProdressChanged?.Invoke(this, new IHasProgress.OnProdressChangedEventArges { progressNormalized = (float)cuttingProgress / curCuttingRecipe_SO.maxCuttingProgress });
            //���Ŷ���
            OnCut?.Invoke(this, EventArgs.Empty);
            //�в�����
            OnAnyCut?.Invoke(this, EventArgs.Empty);
            if (cuttingProgress < curCuttingRecipe_SO.maxCuttingProgress)
                return;
            myKitchenObject.DestorySelf();
            //��̬����
            KitchenObject.PlaceKitchenObject(curCuttingRecipe_SO.output, this);
        }
    }

    private bool CanCut(KitchenObject_SO kitchenObject_SO)
    {
        curCuttingRecipe_SO = GetCuttingRecipe_SOByInput(kitchenObject_SO);
        return curCuttingRecipe_SO != null;
    }

    private CuttingRecipe_SO GetCuttingRecipe_SOByInput(KitchenObject_SO kitchenObject_SO)
    {
        foreach (CuttingRecipe_SO cuttingRecipe_SO in cuttingRecipe_SOArray)
            if (cuttingRecipe_SO.input == kitchenObject_SO)
                return cuttingRecipe_SO;
        return null;
    }
}
