using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    private Image barImage;
    // 实现IHasProgress接口就可以用这个进度条
    private IHasProgress hasProgress;

    private void Awake()
    {
        barImage = transform.Find("Bar").GetComponent<Image>();
        barImage.fillAmount = 0;
        hasProgress = GetComponentInParent<IHasProgress>();
        ShowBar(false);
    }

    private void Start()
    {
        hasProgress.OnProdressChanged += HasProgress_OnProdressChanged;
    }

    private void HasProgress_OnProdressChanged(object sender, IHasProgress.OnProdressChangedEventArges e)
    {
        barImage.fillAmount = e.progressNormalized;
        if (barImage.fillAmount > 0.99)
        {
            barImage.fillAmount = 0;
            ShowBar(false);
        }
        else
            ShowBar(true);
    }

    public void ShowBar(bool isShow)
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(isShow);
    }
}

