using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public event EventHandler OnPickupSomething;

    // �ı���ѡ�еĹ�̨��visual���¼�
    public event EventHandler<OnCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnCounterChangedEventArgs : EventArgs
    {
        public BaseCounter counter;
    }

    private static Player instance;
    public static Player Instance => instance;
    private void Awake()
    {
        instance = this;
    }

    private Vector3 moveTowards;
    private bool isWalking;
    public bool IsWalking => isWalking;

    private Vector3 lastInteraction;    //��ǰ�ĳ���
    public LayerMask clearCounterLayerMask;    //��̨�Ĳ㼶
    private BaseCounter selectedCounter;    //��ѡ�еĹ�̨

    private Transform kitchenObjectPoint;    //������Ʒ���е�
    private KitchenObject myKitchenObject;    //���е���Ʒ
    

    void Start()
    {
        //GameInput �İ���Event����OnInteractAction
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;

        kitchenObjectPoint = transform.Find("KitchenObjectPoint");
    }

    void Update()
    {
        if (!GameManager.Instance.IsGamePlaying())
            return;

        HandleInteractions();
        HandleMovement();
    }

    //����Player�ƶ��ķ���    #CapsuleCast,Vector3.Slerp
    private void HandleMovement()
    {
        // get moveTowards from GameInput
        moveTowards = GameInput.Instance.MoveTowards;
        isWalking = moveTowards == Vector3.zero ? false : true;
        //��ת����
        transform.forward = Vector3.Slerp(transform.forward, moveTowards, 14 * Time.deltaTime);
        //                                     CapsuleCast    bottom                               top                                    radious   direction       distance
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 2f, 0.7f, moveTowards, 7 * Time.deltaTime);
        //�����ǽ�ж��ܷ񳯴�ֱ�����ƶ������޸�moveTowards
        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveTowards.x, 0, 0).normalized;
            //                   x direction has input and x direction do not have wall
            canMove = (moveDirX.x < -0.5f || moveDirX.x > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * 2f, 0.7f, moveDirX, 7 * Time.deltaTime);
            if (canMove)
                moveTowards = moveDirX;
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveTowards.z).normalized;
                float playerHight = 2f;
                float playerRadius = 0.7f;
                canMove = (moveDirZ.z < -0.5f || moveDirZ.z > 0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, moveDirZ, 7 * Time.deltaTime);
                if (canMove)
                    moveTowards = moveDirZ;
            }
        }
        if (canMove)
            transform.position += moveTowards.normalized * 7 * Time.deltaTime;
    }

    

    /// <summary>
    /// ������ǰ���Ĺ�̨��������selectedCounter
    /// </summary>
    private void HandleInteractions()
    {
        // get moveTowards from GameInput
        Vector3 moveTowards = GameInput.Instance.MoveTowards;
        if (moveTowards != Vector3.zero)
            lastInteraction = moveTowards;
        RaycastHit raycastHit;
        //�ò㼶�������߼�������
        if (Physics.Raycast(transform.position, lastInteraction, out raycastHit, 2f, clearCounterLayerMask))
        {
            BaseCounter counter = raycastHit.transform.GetComponent<BaseCounter>();
            if (counter != null)
            {
                if (counter != selectedCounter)
                    SetSelectedCounter(counter);
            }
            else
                SetSelectedCounter(null);
        }
        else
            SetSelectedCounter(null);
    }
    private void SetSelectedCounter(BaseCounter _counter)
    {
        selectedCounter = _counter;
        OnSelectedCounterChanged?.Invoke(this, new OnCounterChangedEventArgs { counter = selectedCounter });
    }


    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())
            return;
        if (selectedCounter != null)
            selectedCounter.InteractAlternate(this);
    }
    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsGamePlaying())
            return;
        if (selectedCounter != null)
            selectedCounter.Interact(this);
    }


    // ʵ��IKitchenObjectParent����
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        myKitchenObject = kitchenObject;
        if (kitchenObject != null)
            OnPickupSomething?.Invoke(this, EventArgs.Empty);
    }
    public Transform GetKitchenObjectPoint() => kitchenObjectPoint;
    public KitchenObject GetKitchenObject() => myKitchenObject;
    public bool HasKitchenObject() => myKitchenObject != null;

}
