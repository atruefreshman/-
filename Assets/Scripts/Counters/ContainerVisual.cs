using UnityEngine;

public class ContainerVisual : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GetComponentInParent<ContainerCounter>().OnPlayerGrabbedObject += ContainerVisual_OnPlayerGrabbedObject;
    }

    private void ContainerVisual_OnPlayerGrabbedObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger("OpenClose");
    }
}
