using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    // �����ѡ�еĹ�̨������ʱ��ÿ����̨�����ѡ�еĹ�̨�ǲ����Լ�
    private void Player_OnSelectedCounterChanged(object sender, Player.OnCounterChangedEventArgs e)
    {
        if (e.counter == GetComponentInParent<BaseCounter>())
            Show(true);
        else
            Show(false);
    }

    private void Show(bool isShow) 
    {
        foreach(Transform child in transform)
            child.gameObject.SetActive(isShow);
    }

}
