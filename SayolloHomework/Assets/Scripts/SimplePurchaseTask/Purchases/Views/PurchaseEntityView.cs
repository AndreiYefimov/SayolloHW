using AndriiYefimov.SayolloHW2.Purchases.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AndriiYefimov.SayolloHW2.Purchases.Views
{
    public class PurchaseEntityView : MonoBehaviour, IPurchasable
    {
        [SerializeField] protected Button purchaseButton;
        
        public void AddPurchaseListener(UnityAction purchaseCallback)
        {
            purchaseButton.onClick.AddListener(purchaseCallback);
        }

        public void RemovePurchaseListener(UnityAction purchaseCallback)
        {
            purchaseButton.onClick.RemoveListener(purchaseCallback);
        }
    }
}