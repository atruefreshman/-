using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    // Á£×ÓÐ§¹û
    private GameObject sizzlingParticles;
    // visual
    private GameObject stoveOnVisual;

    private void Awake()
    {
        sizzlingParticles = transform.Find("SizzlingParticles").gameObject;
        stoveOnVisual = transform.Find("StoveOnVisual").gameObject;
    }

    private void Start()
    {
        GetComponentInParent<StoveCounter>().OnStateChangeed += StoveCounterVisual_OnStateChangeed; ;
    }

    private void StoveCounterVisual_OnStateChangeed(object sender, StoveCounter.OnStateChangeedEventArges e)
    {
        sizzlingParticles.SetActive(e.isFrying);
        stoveOnVisual.SetActive(e.isFrying);
    }
}
