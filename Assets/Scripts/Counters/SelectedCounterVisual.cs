using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    // 当玩家选中的柜台发生变时，每个柜台检查新选中的柜台是不是自己
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
