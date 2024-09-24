using System;

public interface IHasProgress
{
    public event EventHandler<OnProdressChangedEventArges> OnProdressChanged;
    public class OnProdressChangedEventArges : EventArgs
    {
        public float progressNormalized;
    }
}
