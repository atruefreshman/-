using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GetComponentInParent<CuttingCounter>().OnCut += CuttingCounterVisual_OnCut;
    }

    private void CuttingCounterVisual_OnCut(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Cut");
    }
}
