using System;
using AndriiYefimov.SayolloHW2.Models;
using UnityEngine;

namespace AndriiYefimov.SayolloHW2.Delegates
{
    public class Delegate
    {
        public delegate void GetItemDetails(string itemId, Action<PurchaseItemModel> callback);
        
        public delegate void GetItemProfileIcon(string itemId, Action<Sprite> callback);
        
        public delegate void PurchaseItem(string itemId);
    }
}