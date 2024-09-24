using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipRefs_SO", menuName = "ScriptableObject/AudioClipRefs")]
public class AudioClipRefs_SO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliverySuccess;
    public AudioClip[] footstep;
    public AudioClip[] objectDrop;
    public AudioClip[] onjectPickup;
    public AudioClip stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
