using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance => instance;

    private const string SOUND_EFFECT_VOLUME = "SoundEffectVolume";
    private const string MUSIC_VOLUME = "MusicVolume";

    // 存放的音频剪辑
    [SerializeField] private AudioClipRefs_SO audioClipRefs_SO;
    // 背景音乐音频播放器
    [SerializeField] private AudioSource bgm;

    // 音效音量
    private float soundEffectvolume=0.5f;
    public float SoundEffectVolume 
    {
        set 
        {
            soundEffectvolume = value;
            PlayerPrefs.SetFloat(SOUND_EFFECT_VOLUME, soundEffectvolume);
            PlayerPrefs.Save();
        }
        get => soundEffectvolume;
    }
    // 音乐音量
    private float musicVolume = 0.5f;
    public float MusicVolume 
    { 
        set 
        {
            musicVolume = value; 
            bgm.volume = musicVolume; 
            PlayerPrefs.SetFloat (MUSIC_VOLUME, musicVolume);
            PlayerPrefs.Save();
        } 
    }
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetVolume();
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickupSomething += Player_OnPickupSomething;
        BaseCounter.OnAnyObjectPlaced += BaseCounter_OnAnyObjectPlaced;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefs_SO.trash, (sender as TrashCounter).transform.position,soundEffectvolume);
    }

    private void BaseCounter_OnAnyObjectPlaced(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefs_SO.objectDrop, (sender as BaseCounter).transform.position,soundEffectvolume);
    }

    private void Player_OnPickupSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefs_SO.onjectPickup, Player.Instance.transform.position,soundEffectvolume);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefs_SO.chop, (sender as CuttingCounter).transform.position, soundEffectvolume);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefs_SO.deliveryFail, DeliveryCounter.Instance.transform.position,soundEffectvolume);
    }
    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefs_SO.deliverySuccess, DeliveryCounter.Instance.transform.position, soundEffectvolume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 0.5f)
    {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 0.5f)
    {
        // 在特定地点播放一次音频
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    public void PlayFootStepSound(Vector3 position) 
    {
        PlaySound(audioClipRefs_SO.footstep, position, soundEffectvolume);
    }

    private void SetVolume() 
    {
        if(PlayerPrefs.HasKey(SOUND_EFFECT_VOLUME))
            soundEffectvolume = PlayerPrefs.GetFloat(SOUND_EFFECT_VOLUME);
        if (PlayerPrefs.HasKey(MUSIC_VOLUME)) 
        {
            musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME);
            bgm.volume=musicVolume;
        }
        // 设置UI
        OptionsUI.Instance.Setup(soundEffectvolume,musicVolume);
    }
}
