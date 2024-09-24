using UnityEngine;

public class ResetStaticDateManager : MonoBehaviour
{
    // 每次游戏开始清除静态事件的订阅，防止出错
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}
