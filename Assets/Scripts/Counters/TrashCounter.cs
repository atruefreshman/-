using System;

public class TrashCounter : BaseCounter
{
    // SoundManagerÉùÒôÊÂ¼þ
    public static event EventHandler OnAnyObjectTrashed;
    new public static void ResetStaticData() => OnAnyObjectTrashed = null;

    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
            player.GetKitchenObject().DestorySelf();
        OnAnyObjectTrashed?.Invoke(this,EventArgs.Empty);
    }
}
