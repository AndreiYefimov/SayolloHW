using UnityEngine.Events;

namespace AndriiYefimov.SayolloHW2.Purchases.Interfaces
{
    public interface IPurchasable
    {
        void AddPurchaseListener(UnityAction purchaseCallback);
        void RemovePurchaseListener(UnityAction purchaseCallback);
    }
}