using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Binding
{
    Move_Up,
    Move_Down,
    Move_Left,
    Move_Right,
    Interact,
    InteractAlternate,
    Pause,
    Gamepad_Interact,
    Gamepad_InteractAlternate,
    Gamepad_Pause,
}

public class GameInput : SingletonMonoBehaviour<GameInput>
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;

    private const string BINDING_JASON = "BindingJason";

    PlayerInputAction playerInputAction;
    private Vector2 moveTowards;
    public Vector3 MoveTowards { get => new Vector3(moveTowards.x, 0, moveTowards.y).normalized; }

    protected override void Awake()
    {
        base.Awake();

        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();    //����playerInputAction.Player

        //���ذ�����
        if (PlayerPrefs.HasKey(BINDING_JASON))
            playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(BINDING_JASON));

        //inputSystem buttom action,�������¼��İ�
        playerInputAction.Player.Interact.performed += Interact_performed;
        playerInputAction.Player.InteractAltemate.performed += InteractAltemate_performed;
        playerInputAction.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        playerInputAction.Player.Interact.performed -= Interact_performed;
        playerInputAction.Player.InteractAltemate.performed -= InteractAltemate_performed;
        playerInputAction.Player.Pause.performed -= Pause_performed;
        playerInputAction.Dispose();
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAltemate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {   //��Ҫ����F�в�
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {   //����E���ô˷��������¼�
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    void Update()
    {
        //Vector2��ֵ�Ķ�ȡ
        moveTowards = playerInputAction.Player.Move.ReadValue<Vector2>();
    }

    public string GetBindingText(Binding binding)
    {   //Binding is enum
        switch (binding)
        {
            case Binding.Interact:                       //ToDisplayString return short name , ToString return long name
                return playerInputAction.Player.Interact.bindings[0].ToDisplayString();
            case Binding.InteractAlternate:
                return playerInputAction.Player.InteractAltemate.bindings[0].ToDisplayString();
            case Binding.Pause:
                return playerInputAction.Player.Pause.bindings[0].ToDisplayString();
            case Binding.Move_Up:
                return playerInputAction.Player.Move.bindings[1].ToDisplayString();    //���ϰ������Ϸ�Binging�±���㿪ʼ
            case Binding.Move_Down:
                return playerInputAction.Player.Move.bindings[2].ToDisplayString();
            case Binding.Move_Left:
                return playerInputAction.Player.Move.bindings[3].ToDisplayString();
            case Binding.Move_Right:
                return playerInputAction.Player.Move.bindings[4].ToDisplayString();
                //�ֱ�
            case Binding.Gamepad_Interact:
                return playerInputAction.Player.Interact.bindings[1].ToDisplayString();
            case Binding.Gamepad_InteractAlternate:
                return playerInputAction.Player.InteractAltemate.bindings[1].ToDisplayString();
            case Binding.Gamepad_Pause:
                return playerInputAction.Player.Pause.bindings[1].ToDisplayString();
            default:
                return null;
        }
    }

    //���°󶨰����ķ���
    public void RebingBinging(Binding binding, Action Callback)
    {
        playerInputAction.Player.Disable();   

        // �õ���Ҫ�޸ĵ�binding
        InputAction inputAction = null;
        int bindingIndex = 0;
        switch (binding)
        {
            case Binding.Move_Up:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputAction.Player.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputAction.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInputAction.Player.InteractAltemate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputAction.Player.Pause;
                bindingIndex = 0;
                break;
            case Binding.Gamepad_Interact:
                inputAction = playerInputAction.Player.Interact;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_InteractAlternate:
                inputAction = playerInputAction.Player.InteractAltemate;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Pause:
                inputAction = playerInputAction.Player.Pause;
                bindingIndex = 1;
                break;
        }

        // �������°�
        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            // �ͷŻص�����ռ�õ���Դ
            callback.Dispose();    

            // �¼���ɻص�
            Callback();    

            playerInputAction.Player.Enable();

            // �Ѱ�������Ϣת��Ϊjson�����ļ�
            PlayerPrefs.SetString(BINDING_JASON, playerInputAction.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();

        }).Start();

    }
}
