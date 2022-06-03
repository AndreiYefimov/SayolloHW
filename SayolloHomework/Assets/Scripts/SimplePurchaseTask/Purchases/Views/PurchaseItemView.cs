using AndriiYefimov.SayolloHW2.Purchases.Interfaces;

namespace AndriiYefimov.SayolloHW2.Purchases.Views
{
    public class PurchaseItemView : PurchaseEntityView, IPurchaseItemView
    {
        private IPurchaseItemDetailsView _itemDetailsView;
        
        private void OnDestroy()
        {
            RemoveListeners();
        }

        public void Init(IPurchaseItemDetailsView itemDetailsView)
        {
            _itemDetailsView = itemDetailsView;
            AddListeners();
        }

        private void AddListeners()
        {
            AddPurchaseListener(OnPurchaseClicked);
        }

        private void RemoveListeners()
        {
            RemovePurchaseListener(OnPurchaseClicked);
        }

        private void OnPurchaseClicked()
        {
            _itemDetailsView?.SetVisibleState(true);
        }
    }
}