using System;
using AndriiYefimov.SayolloHW2.Purchases.Models;
using UnityEngine;

namespace AndriiYefimov.SayolloHW2.Delegates
{
    public static class Delegate
    {
        public delegate void GetItemDetails(string itemId, Action<PurchaseItemModel> callback);
        
        public delegate void GetItemProfileIcon(string itemId, Action<Sprite> callback);
        
        public delegate void PurchaseItem(string itemId);
    }
}