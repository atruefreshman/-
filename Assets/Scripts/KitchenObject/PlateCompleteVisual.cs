using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObject_SO_GameObject
    {
        public KitchenObject_SO kitchenObject_SO;
        public GameObject visualObject;
    }

    PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObject_SO_GameObject> kitchenObject_SO_GameObjectList;

    private void Start()
    {
        plateKitchenObject = GetComponentInParent<PlateKitchenObject>();

        plateKitchenObject.OnIngredientAdded += (object sender, PlateKitchenObject.OnIngredientAddedEventArges e)=> 
        {
            foreach (KitchenObject_SO_GameObject kitchenObject_SO_GameObject in kitchenObject_SO_GameObjectList)
                if (kitchenObject_SO_GameObject.kitchenObject_SO == e.kitchenObject_SO)
                    kitchenObject_SO_GameObject.visualObject.SetActive(true);
        };

        foreach (KitchenObject_SO_GameObject kitchenObject_SO_GameObject in kitchenObject_SO_GameObjectList)
            kitchenObject_SO_GameObject.visualObject.SetActive(false);
    }
}
