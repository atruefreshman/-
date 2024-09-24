using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    // 两个用于控制visual的事件
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateTaken;
    // 要生成在player手中的盘子
    [SerializeField] private KitchenObject_SO plateKitchenObject;

    private float spawnTimer;
    private int plateSpawnedAmount;    // 已生成的盘子的数量
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

    // 主要交互
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject() || plateSpawnedAmount <= 0)
            return;

        plateSpawnedAmount--;
        OnPlateTaken?.Invoke(this, EventArgs.Empty);
        KitchenObject.PlaceKitchenObject(plateKitchenObject, player);
    }
}
