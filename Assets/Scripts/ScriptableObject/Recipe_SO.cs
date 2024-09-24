using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe_SO", menuName = "ScriptableObject/Recipe")]
public class Recipe_SO : ScriptableObject
{
    public List<KitchenObject_SO> kitchenObject_SOList;
    public string recipeName;
}
