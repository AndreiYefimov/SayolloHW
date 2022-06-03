using AndriiYefimov.SayolloHW2.Interfaces;
using AndriiYefimov.SayolloHW2.Purchases.Models;
using UnityEngine;
using UnityEngine.Events;

namespace AndriiYefimov.SayolloHW2.Purchases.Interfaces
{
    public interface IPurchaseItemDetailsView : IVisible
    {
        void SetDetails(PurchaseItemModel itemDetails);
        void SetProfileIcon(Sprite sprite);
        void AddCloseListener(UnityAction purchaseCallback);
        void RemoveCloseListener(UnityAction purchaseCallback);
    }
}