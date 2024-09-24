using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArges> OnIngredientAdded;
    public class OnIngredientAddedEventArges : EventArgs
    {
        public KitchenObject_SO kitchenObject_SO;
    }

    // 能放到盘子里的物品
    [SerializeField] private List<KitchenObject_SO> validKitchenObject_SOList;
    // 已经加到盘子里的物品
    private List<KitchenObject_SO> addedKitchenObject_SOList;

    private void Awake()
    {
        addedKitchenObject_SOList = new List<KitchenObject_SO>();
    }

    public bool TryAddIngredient(KitchenObject_SO kitchenObject_SO)
    {
        if (addedKitchenObject_SOList.Contains(kitchenObject_SO) || !validKitchenObject_SOList.Contains(kitchenObject_SO))
            return false;

        addedKitchenObject_SOList.Add(kitchenObject_SO);
        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArges { kitchenObject_SO = kitchenObject_SO });
        return true;
    }

    public List<KitchenObject_SO> GetAddedKitchenObject_SOList() => addedKitchenObject_SOList;
}
