using AndriiYefimov.SayolloHW2.Delegates;
using AndriiYefimov.SayolloHW2.Purchases.Models;
using AndriiYefimov.SayolloHW2.Purchases.Views;
using UnityEngine;

namespace AndriiYefimov.SayolloHW2.Purchases
{
    public class PurchaseItem : MonoBehaviour
    {
        [SerializeField] private PurchaseItemView purchaseItemView;
        [SerializeField] private PurchaseItemDetailsView purchaseItemDetailsView;

        private string _itemId;
        
        public event Delegate.GetItemDetails ItemDetailsRequest;
        public event Delegate.GetItemProfileIcon ProfileIconRequest;
        public event Delegate.PurchaseItem ItemPurchaseRequested;

        private void Awake()
        {
            InitComponents();
            AddListeners();
        }

        private void InitComponents()
        {
            purchaseItemView.Init(purchaseItemDetailsView);
        }

        public void Init(string itemId)
        {
            _itemId = itemId;
        }

        private void AddListeners()
        {
            purchaseItemView.AddPurchaseListener(OnPreviewPurchaseClicked);
            purchaseItemDetailsView.AddPurchaseListener(OnPurchaseClicked);
        }

        private void OnPurchaseClicked()
        {
            ItemPurchaseRequested?.Invoke(_itemId);
        }

        private void OnPreviewPurchaseClicked()
        {
            ItemDetailsRequest?.Invoke(_itemId, OnDetailsReceived);
        }

        private void OnDetailsReceived(PurchaseItemModel itemDetails)
        {
            Debug.Log($"OnDetailsReceived: {itemDetails.item_name}");
            purchaseItemDetailsView.SetDetails(itemDetails);
            
            ProfileIconRequest?.Invoke(itemDetails.item_image, OnProfileIconReceived);
        }

        private void OnProfileIconReceived(Sprite icon)
        {
            Debug.Log($"OnDetailsReceived | icon null: {icon == null}");
            purchaseItemDetailsView.SetProfileIcon(icon);
        }
    }
}