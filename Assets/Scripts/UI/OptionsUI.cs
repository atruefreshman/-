using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI instance;
    public static OptionsUI Instance => instance;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundEffectSlider;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button MoveUpButton;
    [SerializeField] private Button MoveDownButton;
    [SerializeField] private Button MoveLeftButton;
    [SerializeField] private Button MoveRightButton;
    [SerializeField] private Button InteractButton;
    [SerializeField] private Button InteractAltButton;
    [SerializeField] private Button PauseButton;
    [SerializeField] private Button GamepadInteractButton;
    [SerializeField] private Button GamepadInteractAltButton;
    [SerializeField] private Button GamepadPauseButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;
    [SerializeField] private Transform rebindingUI;

    private void Awake()
    {
        instance = this;
        // 音量
        musicSlider.onValueChanged.AddListener((float volume) => { SoundManager.Instance.MusicVolume = volume; });
        soundEffectSlider.onValueChanged.AddListener((float volume) => { SoundManager.Instance.SoundEffectVolume = volume; });
        // 关闭界面
        closeButton.onClick.AddListener(() => { GamePauseUI.Instance.Show(); gameObject.SetActive(false); });
        // 按键绑定
        MoveUpButton.onClick.AddListener(() => { RebingBing(Binding.Move_Up); });
        MoveDownButton.onClick.AddListener(() => { RebingBing(Binding.Move_Down); });
        MoveLeftButton.onClick.AddListener(() => { RebingBing(Binding.Move_Left); });
        MoveRightButton.onClick.AddListener(() => { RebingBing(Binding.Move_Right); });
        InteractButton.onClick.AddListener(() => { RebingBing(Binding.Interact); });
        InteractAltButton.onClick.AddListener(() => { RebingBing(Binding.InteractAlternate); });
        PauseButton.onClick.AddListener(() => { RebingBing(Binding.Pause); });
        GamepadInteractButton.onClick.AddListener(() => { RebingBing(Binding.Gamepad_Interact); });
        GamepadInteractAltButton.onClick.AddListener(() => { RebingBing(Binding.Gamepad_InteractAlternate); });
        GamepadPauseButton.onClick.AddListener(() => { RebingBing(Binding.Gamepad_Pause); });

        rebindingUI.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Start()
    {
        ShowBundingText();
    }

    private void ShowBundingText()
    {
        moveUpText.text = GameInput.Instance.GetBindingText(Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(Binding.Pause);
        gamepadInteractText.text = GameInput.Instance.GetBindingText(Binding.Gamepad_Interact);
        gamepadInteractAltText.text = GameInput.Instance.GetBindingText(Binding.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(Binding.Gamepad_Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        MoveUpButton.Select();
    }

    public void Setup(float soundEffectVolume, float musicVolume)
    {
        musicSlider.value = musicVolume;
        soundEffectSlider.value = soundEffectVolume;
        gameObject.SetActive(false);
    }


    private void RebingBing(Binding binding)
    {
        rebindingUI.gameObject.SetActive(true);

        GameInput.Instance.RebingBinging(binding, () =>
        {
            rebindingUI.gameObject.SetActive(false);

            ShowBundingText();
        });
    }


}
