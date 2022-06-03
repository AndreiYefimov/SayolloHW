using System.Collections.Generic;
using AndriiYefimov.SayolloHW2.Delegates;
using UnityEngine;
using PurchaseItem = AndriiYefimov.SayolloHW2.Purchases.PurchaseItem;

namespace AndriiYefimov.SayolloHW2.Test
{
    public class TestPurchaseManager : MonoBehaviour
    {
        [SerializeField] private List<PurchaseItem> purchaseItems;

        private string _testItemId = "";
        
        public void Init(Delegate.GetItemDetails getDetailsCallback,
            Delegate.GetItemProfileIcon getIconCallback, Delegate.PurchaseItem purchaseItemCallback)
        {
            InitItemsIds();
            AddItemsListeners(getDetailsCallback, getIconCallback, purchaseItemCallback);
        }

        private void AddItemsListeners(Delegate.GetItemDetails getDetailsCallback,
            Delegate.GetItemProfileIcon getIconCallback, Delegate.PurchaseItem purchaseItemCallback)
        {
            // set all the callbacks for purchase item events
            foreach (var item in purchaseItems)
            {
                AddItemListeners(getDetailsCallback, getIconCallback, purchaseItemCallback, item);
            }
        }

        private static void AddItemListeners(Delegate.GetItemDetails getDetailsCallback,
            Delegate.GetItemProfileIcon getIconCallback, Delegate.PurchaseItem purchaseItemCallback, PurchaseItem item)
        {
            item.ItemDetailsRequest += getDetailsCallback;
            item.ProfileIconRequest += getIconCallback;
            item.ItemPurchaseRequested += purchaseItemCallback;
        }

        private void InitItemsIds()
        {
            foreach (var item in purchaseItems)
                item.Init(_testItemId);
        }
    }
}