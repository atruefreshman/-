using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_WALKING, false);
    }

    void Update() 
    {
        animator.SetBool(IS_WALKING,Player.Instance.IsWalking);
    }
}
