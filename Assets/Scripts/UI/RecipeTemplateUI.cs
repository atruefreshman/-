using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTemplateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Transform recipeIconsUI;

    public void Setup(Recipe_SO recipe_SO) 
    {
        foreach(Transform child in recipeIconsUI)
            child.gameObject.SetActive(false);  
        nameText.text = recipe_SO.name;
        for (int i = 0; i < recipe_SO.kitchenObject_SOList.Count; i++) 
        {
            recipeIconsUI.GetChild(i).GetComponent<Image>().sprite = recipe_SO.kitchenObject_SOList[i].sprite;
            recipeIconsUI.GetChild(i).gameObject.SetActive(true);
        }
    }
}
