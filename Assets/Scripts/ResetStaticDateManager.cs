using UnityEngine;

public class ResetStaticDateManager : MonoBehaviour
{
    // ÿ����Ϸ��ʼ�����̬�¼��Ķ��ģ���ֹ����
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashCounter.ResetStaticData();
    }
}
