using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    // �������ڿ���visual���¼�
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateTaken;
    // Ҫ������player���е�����
    [SerializeField] private KitchenObject_SO plateKitchenObject;

    private float spawnTimer;
    private int plateSpawnedAmount;    // �����ɵ����ӵ�����
    private int maxPlateAmount=4;

    private void Update()
    {
        if (!GameManager.Instance.IsGamePlaying())
            return;

        if (plateSpawnedAmount < maxPlateAmount)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > 4)
            {
                spawnTimer = 0;
                plateSpawnedAmount++;
                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
        else
            spawnTimer = 0;
    }

    // ��Ҫ����
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() || plateSpawnedAmount <= 0)
            return;

        plateSpawnedAmount--;
        OnPlateTaken?.Invoke(this, EventArgs.Empty);
        KitchenObject.PlaceKitchenObject(plateKitchenObject, player);
    }
}
