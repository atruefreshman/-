using System;
using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgress
{
    // ����visual�������¼�
    public event EventHandler<OnStateChangeedEventArges> OnStateChangeed;
    public class OnStateChangeedEventArges : EventArgs { public bool isFrying; }
    // ������UI�¼�
    public event EventHandler<IHasProgress.OnProdressChangedEventArges> OnProdressChanged;

    // ���ת����ϵ
    [SerializeField] private FryingRecipe_SO[] fryingRecipe_SOArray;

    private float fryingProgress;
    private FryingRecipe_SO fryingRecipe_SO;


    private void Update()
    {
        if (myKitchenObject != null)
        {
            fryingProgress += Time.deltaTime;
            if (fryingRecipe_SO != null)    //
            {
                // ������
                OnProdressChanged?.Invoke(this, new IHasProgress.OnProdressChangedEventArges { progressNormalized = fryingProgress / fryingRecipe_SO.maxFryingTime });
                if (fryingProgress >= fryingRecipe_SO.maxFryingTime)
                {
                    fryingProgress = 0;
                    myKitchenObject.DestorySelf();
                    KitchenObject.PlaceKitchenObject(fryingRecipe_SO.output, this);
                    // �Ӿ�������
                    bool isFrying = CanBurn(fryingRecipe_SO.output);
                    OnStateChangeed?.Invoke(this, new OnStateChangeedEventArges { isFrying = isFrying });
                }
            }
        }
    }

    // ��Ҫ����
    public override void Interact(Player player)
    {
        if (myKitchenObject == null)    //�Լ���û����Ʒ
        {
            if (player.HasKitchenObject()&& CanBurn(player.GetKitchenObject().GetKitchenObject_SO))    //�����������Ʒ,���������Ʒ�ǿ����յ�,���ܷŵ���̨��
            {
                fryingProgress = 0;
                player.GetKitchenObject().SetPointParent(this);
                OnStateChangeed?.Invoke(this, new OnStateChangeedEventArges { isFrying = true });
            }
        }
        else    //�Լ�������Ʒ���������û��Ʒ
        {
            if (!player.HasKitchenObject())
            {
                myKitchenObject.SetPointParent(player);
                OnStateChangeed?.Invoke(this, new OnStateChangeedEventArges { isFrying = false });
                OnProdressChanged?.Invoke(this, new IHasProgress.OnProdressChangedEventArges { progressNormalized = 1 });
            }
            else
            {            //�����������Ʒ����������                     
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                    if (plateKitchenObject.TryAddIngredient(myKitchenObject.GetKitchenObject_SO))    //����Ʒ�ӵ�������
                    {
                        myKitchenObject.DestorySelf();
                        OnStateChangeed?.Invoke(this, new OnStateChangeedEventArges { isFrying = false });
                        OnProdressChanged?.Invoke(this, new IHasProgress.OnProdressChangedEventArges { progressNormalized = 1 });
                    }    
            }
        }
    }

    private bool CanBurn(KitchenObject_SO kitchenObject_SO)
    {
        fryingRecipe_SO = GetFryingRecipe_SOByInput(kitchenObject_SO);
        return fryingRecipe_SO != null;
    }

    private FryingRecipe_SO GetFryingRecipe_SOByInput(KitchenObject_SO kitchenObject_SO)
    {
        foreach (FryingRecipe_SO frytingRecipe_SO in fryingRecipe_SOArray)
            if (frytingRecipe_SO.input == kitchenObject_SO)
                return frytingRecipe_SO;
        return null;
    }
}
