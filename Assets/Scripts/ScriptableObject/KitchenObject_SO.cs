using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObject_SO", menuName = "ScriptableObject/KitchenObject")]
public class KitchenObject_SO : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string objectName;
}
