using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform kitchenObjectPoint;
    // 盘子预制体
    [SerializeField] private GameObject plateVisualPrefab;

    // 存储生成的visual的list
    private List<GameObject> plateVisualGameObjectList;

    private PlatesCounter platesCounter;

    private void Start()
    {
        platesCounter = GetComponentInParent<PlatesCounter>();
        plateVisualGameObjectList = new List<GameObject>();

        platesCounter.OnPlateSpawn += PlatesCounterVisual_OnPlateSpawn;
        platesCounter.OnPlateTaken += PlatesCounter_OnPlateTaken;
    }

    // 销毁盘子的visual
    private void PlatesCounter_OnPlateTaken(object sender, System.EventArgs e)
    {
        GameObject plateVisualObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateVisualObject);
        Destroy(plateVisualObject);
    }

    // 生成盘子的visual
    private void PlatesCounterVisual_OnPlateSpawn(object sender, System.EventArgs e)
    {
        GameObject newPlateVisual = Instantiate(plateVisualPrefab, kitchenObjectPoint);
        // 每个新生成的visual都往上放一点
        float plateYOffset = 0.1f;
        newPlateVisual.transform.localPosition = new Vector3(0, plateYOffset * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(newPlateVisual);
    }
}
