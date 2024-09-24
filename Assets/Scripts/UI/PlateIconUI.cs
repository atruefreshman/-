using UnityEngine;
using UnityEngine.UI;

public class PlateIconUI : MonoBehaviour
{
    private Transform iconTemplate;
    PlateKitchenObject plateKitchenObject;

    private void Awake()
    {
        iconTemplate = transform.Find("IconTemplate");
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject = GetComponentInParent<PlateKitchenObject>();
        plateKitchenObject.OnIngredientAdded += PlateIconUI_OnIngredientAdded;
    }

    private void PlateIconUI_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArges e)
    {
        foreach (Transform child in transform)
        {
            if (child == iconTemplate)
                continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObject_SO kitchenObject_SO in plateKitchenObject.GetAddedKitchenObject_SOList())
        {
            Transform icon = Instantiate(iconTemplate, transform);
            icon.gameObject.SetActive(true);
            icon.Find("Icon").GetComponent<Image>().sprite = kitchenObject_SO.sprite;
        }
    }
}
