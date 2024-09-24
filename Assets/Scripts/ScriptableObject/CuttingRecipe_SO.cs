using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipe_SO", menuName = "ScriptableObject/CuttingRecipe")]
public class CuttingRecipe_SO : ScriptableObject
{
    public KitchenObject_SO input;
    public KitchenObject_SO output;
    public int maxCuttingProgress;
}
