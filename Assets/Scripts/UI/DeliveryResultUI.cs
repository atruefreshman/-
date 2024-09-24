using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField] private Image BG;
    [SerializeField] private Image Icon;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color successColor;
    [SerializeField] private Color failureColor;
    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failureSprite;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        StopAllCoroutines();
        BG.color= failureColor;
        Icon.sprite = failureSprite;
        messageText.text = "DELIVERY\nFAILED";
        gameObject.SetActive(true);
        StartCoroutine(WaitToHide());
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        StopAllCoroutines();
        BG.color = successColor;
        Icon.sprite = successSprite;
        messageText.text = "DELIVERY\nSUCCESS";
        gameObject.SetActive(true);
        StartCoroutine(WaitToHide());
    }

    IEnumerator WaitToHide() 
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.SetActive(false);
    }

}
