using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform kitchenObjectPoint;
    // ����Ԥ����
    [SerializeField] private GameObject plateVisualPrefab;

    // �洢���ɵ�visual��list
    private List<GameObject> plateVisualGameObjectList;

    private PlatesCounter platesCounter;

    private void Start()
    {
        platesCounter = GetComponentInParent<PlatesCounter>();
        plateVisualGameObjectList = new List<GameObject>();

        platesCounter.OnPlateSpawn += PlatesCounterVisual_OnPlateSpawn;
        platesCounter.OnPlateTaken += PlatesCounter_OnPlateTaken;
    }

    // �������ӵ�visual
    private void PlatesCounter_OnPlateTaken(object sender, System.EventArgs e)
    {
        GameObject plateVisualObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateVisualObject);
        Destroy(plateVisualObject);
    }

    // �������ӵ�visual
    private void PlatesCounterVisual_OnPlateSpawn(object sender, System.EventArgs e)
    {
        GameObject newPlateVisual = Instantiate(plateVisualPrefab, kitchenObjectPoint);
        // ÿ�������ɵ�visual�����Ϸ�һ��
        float plateYOffset = 0.1f;
        newPlateVisual.transform.localPosition = new Vector3(0, plateYOffset * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(newPlateVisual);
    }
}
