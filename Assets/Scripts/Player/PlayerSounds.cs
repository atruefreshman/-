using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    private Player player;
    private float footSteptime = 0.1f;
    private float footStepTimer;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if (player.IsWalking && footStepTimer <= 0)
        {
            footStepTimer = footSteptime;
            SoundManager.Instance.PlayFootStepSound(transform.position);
        }
    }
}
