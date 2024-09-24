using UnityEngine;

[CreateAssetMenu(fileName = "FryingRecipe_SO", menuName = "ScriptableObject/FryingRecipe")]
public class FryingRecipe_SO : ScriptableObject
{
    public KitchenObject_SO input;
    public KitchenObject_SO output;
    public float maxFryingTime;
}
